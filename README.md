# index-fullstackapp

INDEx Full Stack App

## Project summary

 As a nonprofit staffed by volunteers, INDEx doesn’t have the means of hiring/supporting a web developer. We are giving them a hand by providing a easily maintainable website, even with no coding experience. 

### Additional information about the project

INDEx currently only has a small reference to their organization on their parent companies website (dacnw.org). INDEx plans on extending this reference to an entirely new website hosted under their own domain name. This means developing a website and making minor edits to the existing reference. 

## Installation

### Prerequisites
None

### Add-ons
#### AWS resources
* s3 bucket: index-webapp (configured for public static web hosting)
* API Gateway: (Configured for https GET and PUT to s3 bucket)
* policy: index-editor-role (Allowing API Gateway access to s3 bucket)
### Installation Steps

### Front end
* Download node js from https://nodejs.org/en/download/ choose either Windows or macOS varify installation with terminal command: "node -v" (v16.17.0)
* Next install npm (node package manager) with the following terminal command: "npm install -g npm", varify installation with command: "npm -v" (8.19.1)
* Install the vue CLI (vue command line interface) with terminal command: "npm install -g @vue/cli", varify installation with command:<br/>"vue --version" (@vue/cli 5.0.8)
* cd into INDEX-VUE where package.json and package-lock.json is located and install dependencies using terminal command: "npm install"
* Recommended: Use Visual Studio Code with Vue Language Features (Volar) extension for syntax highlighting.

### Back end
* Download the latest net6.0-windows sdk from https://dotnet.microsoft.com/en-us/download/dotnet/6.0
* Download visual studio 2019 or 2022 from https://visualstudio.microsoft.com/downloads/
* Using Visual studio installer, install workload: ASP.NET and web development
* Using Visual studio installer, install workload: .NET desktop development
* Rename "appsettings.example.json" to appsettings.json and add your endpoint and API key.

### Infrastructure
* Download and install Terraform from the official website: https://www.terraform.io/downloads.html.
* Download AWS CLI: Follow the instructions in the official AWS documentation to install the AWS CLI: https://aws.amazon.com/cli/
* Ensure that you have AWS credentials set up correctly.
* Configure AWS CLI with your AWS Access Key ID, Secret Access Key, default region, and default output format
* Run terraform init, terraform plan and terraform apply to deploy

### Back end packages added
* dotnet add package FontAwesome.Sharp --version 6.2.1

## Functionality
TODO:

## Known Problems

## Contributing
1.Initialize git: `git init`

2.Run the command to ensure you are up to date: `git pull`

3.Direct git to this project by the command: `git remote add origin https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp.git`

4.Branch off the main branch: `git branch main`

5.Switch to the new branch with: `git checkout {brance name}`

6.Commit your changes: `git commit -m 'Added new feature'`

7.Push to the branch: `git push -u origin {branch name}`

## Additional Documentation
  * [Sprint Reports](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/tree/main/Sprint%20Report)
  * [Documentation](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/tree/main/Documentation)
## License

If you haven't already, add a file called `LICENSE.txt` with the text of the appropriate license.
We recommend using the MIT license: <https://choosealicense.com/licenses/mit/>
