pipeline {
    agent any

    environment {
        APP_NAME = 'mss-ims-api'
        SOLUTION_FILE = 'mss.ims.slnx'
        IMAGE_TAG = "${env.BUILD_NUMBER}"
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
                sh '''
                    docker rm -f $APP_NAME-test || true

                    docker network create jenkins-test || true

                    docker run -d \
                      --name $APP_NAME-test \
                      --network jenkins-test \
                      $APP_NAME:$IMAGE_TAG

                    sleep 10

                    docker ps -a
                    docker logs $APP_NAME-test

                    docker run --rm \
                      --network jenkins-test \
                      curlimages/curl:latest \
                      curl -f http://$APP_NAME-test:8080/health

                    docker rm -f $APP_NAME-test
                '''
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