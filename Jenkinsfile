pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Docker Build API') {
            steps {
                sh 'docker build -t mss-ims-api -f mss.ims.api/Dockerfile .'
            }
        }
    }
}