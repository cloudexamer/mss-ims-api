pipeline {
    agent any

    environment {
        APP_NAME = 'mss-ims-api'
        SOLUTION_FILE = 'mss.ims.slnx'
        IMAGE_TAG = "${env.BUILD_NUMBER}"
		AWS_REGION = 'us-east-1'
		ECS_CLUSTER = 'mss-ims-cluster'
		ECS_SERVICE = 'mss-ims-api-service'
		ECS_TASK_FAMILY = 'mss-ims-api-task'
		CONTAINER_NAME = 'mss-ims-api'		
		ECR_REPO_URI = '848347449287.dkr.ecr.us-east-1.amazonaws.com/mss-ims-api'
		DOTNET_SYSTEM_GLOBALIZATION_INVARIANT = '1'
    }

    stages {
        stage('Restore') {
            steps {
                sh 'dotnet restore $SOLUTION_FILE'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build $SOLUTION_FILE --configuration Release --no-restore'
            }
        }

        stage('Test') {
            steps {
                sh 'dotnet test $SOLUTION_FILE --configuration Release --no-build || true'
            }
        }

        stage('Docker Build API') {
            steps {
                sh '''
                    docker build \
                      -t $APP_NAME:$IMAGE_TAG \
                      -t $APP_NAME:latest \
                      -f mss.ims.api/Dockerfile .
                '''
            }
        }

		stage('Smoke Test Container') {
			steps {
				withCredentials([string(credentialsId: 'internal-diagnostics-key', variable: 'INTERNAL_DIAGNOSTICS_KEY')]) {
					sh '''
						docker rm -f $APP_NAME-test || true

						docker network create jenkins-test || true

						docker run -d --name $APP_NAME-test --network jenkins-test -e BUILD_NUMBER=$BUILD_NUMBER -e IMAGE_TAG=$IMAGE_TAG -e INTERNAL_DIAGNOSTICS_KEY=$INTERNAL_DIAGNOSTICS_KEY $APP_NAME:$IMAGE_TAG

						sleep 10

						docker ps -a
						docker logs $APP_NAME-test

						docker run --rm --network jenkins-test curlimages/curl:latest curl -f http://$APP_NAME-test:8080/health

						docker run --rm --network jenkins-test curlimages/curl:latest curl -f -H "X-Internal-Diagnostics-Key: $INTERNAL_DIAGNOSTICS_KEY" http://$APP_NAME-test:8080/internal/version > version.json

						cat version.json

						grep "$BUILD_NUMBER" version.json

						docker rm -f $APP_NAME-test
					'''
				}
			}
		}
		
		stage('Push Image to ECR') {
			steps {
				withCredentials([usernamePassword(credentialsId: 'aws-djacobo', usernameVariable: 'AWS_ACCESS_KEY_ID', passwordVariable: 'AWS_SECRET_ACCESS_KEY')]) {
					sh '''
						aws --version

						aws ecr get-login-password --region $AWS_REGION \
						  | docker login --username AWS --password-stdin $ECR_REPO_URI

						docker tag $APP_NAME:$IMAGE_TAG $ECR_REPO_URI:$IMAGE_TAG
						docker tag $APP_NAME:$IMAGE_TAG $ECR_REPO_URI:latest

						docker push $ECR_REPO_URI:$IMAGE_TAG
						docker push $ECR_REPO_URI:latest
					'''
				}
			}
		}
		
		stage('Deploy to ECS') {
			steps {
				withCredentials([usernamePassword(credentialsId: 'aws-djacobo', usernameVariable: 'AWS_ACCESS_KEY_ID', passwordVariable: 'AWS_SECRET_ACCESS_KEY')]) {
					sh '''
						aws ecs describe-task-definition \
						  --task-definition $ECS_TASK_FAMILY \
						  --region $AWS_REGION \
						  --query taskDefinition > task-definition.json

						python3 - <<'PY'
		import json
		import os

		with open("task-definition.json") as f:
			task_def = json.load(f)

		image = f"{os.environ['ECR_REPO_URI']}:{os.environ['IMAGE_TAG']}"
		container_name = os.environ["CONTAINER_NAME"]

		for container in task_def["containerDefinitions"]:
			if container["name"] == container_name:
				container["image"] = image
				env = container.setdefault("environment", [])

				def set_env(name, value):
					for item in env:
						if item["name"] == name:
							item["value"] = value
							return
					env.append({"name": name, "value": value})

				set_env("BUILD_NUMBER", os.environ["BUILD_NUMBER"])
				set_env("IMAGE_TAG", os.environ["IMAGE_TAG"])

		for field in [
			"taskDefinitionArn",
			"revision",
			"status",
			"requiresAttributes",
			"compatibilities",
			"registeredAt",
			"registeredBy"
		]:
			task_def.pop(field, None)

		with open("new-task-definition.json", "w") as f:
			json.dump(task_def, f)
		PY

						NEW_TASK_DEF_ARN=$(aws ecs register-task-definition \
						  --cli-input-json file://new-task-definition.json \
						  --region $AWS_REGION \
						  --query 'taskDefinition.taskDefinitionArn' \
						  --output text)

						echo "Registered task definition: $NEW_TASK_DEF_ARN"

						aws ecs update-service \
						  --cluster $ECS_CLUSTER \
						  --service $ECS_SERVICE \
						  --task-definition $NEW_TASK_DEF_ARN \
						  --region $AWS_REGION

						aws ecs wait services-stable \
						  --cluster $ECS_CLUSTER \
						  --services $ECS_SERVICE \
						  --region $AWS_REGION
					'''
				}
			}
		}					
    }

    post {
        always {
            sh 'docker rm -f $APP_NAME-test || true'
        }

        success {
            echo 'Pipeline completed successfully.'
        }

        failure {
            echo 'Pipeline failed.'
        }
    }
}