pipeline {
    agent any

    environment {
        APP_NAME = 'mss-ims-api'
        API_PROJECT = 'mss.ims.api/mss.ims.api.csproj'
        IMAGE_TAG = "${env.BUILD_NUMBER}"
		DOTNET_SYSTEM_GLOBALIZATION_INVARIANT = '1'
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release --no-restore'
            }
        }

        stage('Test') {
            steps {
                sh 'dotnet test --configuration Release --no-build'
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

                    docker run -d \
                      --name $APP_NAME-test \
                      -p 8085:80 \
                      $APP_NAME:$IMAGE_TAG

                    sleep 10

                    curl -f http://localhost:8085/health

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