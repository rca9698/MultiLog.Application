pipeline {
    agent any

    environment {
        DOTNET_CLI_HOME = "C:\\Program Files\\dotnet"
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build') {
            steps {
                script {
                    // Restoring dependencies
                    //bat "cd ${DOTNET_CLI_HOME} && dotnet restore"
                    bat "dotnet restore"

                    // Building the application
                    bat "dotnet build --configuration Release"
                }
            }
        }

        stage('Test') {
            steps {
                script {
                    // Running tests
                    bat "dotnet test --no-restore --configuration Release"
                }
            }
        }

        stage('Publish') {
            steps {
                script {
                    // Publishing the application
                    bat "dotnet publish --no-restore --configuration Release --output .\\publish"
                }
            }
        }
       stage('Deploy') {
            steps {
                script {
                    withCredentials([usernamePassword(credentialsId: 'CICDTest', passwordVariable: 'Rakesh@9698', usernameVariable: 'rakesh')]) {
                    powershell '''
                    
                    $credentials = New-Object System.Management.Automation.PSCredential($env:rakesh, (ConvertTo-SecureString $env:Rakesh@9698 -AsPlainText -Force))

                    
                    New-PSDrive -Name C -PSProvider FileSystem -Root "\\\\98.70.9.229\\Hosting_Path\\CICDTest" -Persist -Credential $credentials

                    
                    Copy-Item -Path '.\\publish\\*' -Destination 'C:\' -Force

                    
                    Remove-PSDrive -Name C
                    '''
                }
                }
            }
        } 
    }

    post {
        success {
            echo 'Build, test, and publish successful!'
        }
    }
}
