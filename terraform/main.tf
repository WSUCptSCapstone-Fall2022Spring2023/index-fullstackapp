provider "aws" {
  alias  = "apigateway_account"
  region = "us-east-1"
}

variable "region" {
  description = "AWS region"
  default     = "us-east-1"
}




# Notes
# terraform init
# terraform plan
# terraform apply

#terraform destroy
#Note: if destroying, use the following command to clear s3 contents before running destroy command
#aws s3 rm s3://index-static-website --recursive








#Integration Request change Content Handling to passthrough
#
#
#Integration Response Content handling convert to binary if needed
#
# DEL Header Mappings
# DEL Mapping Templates
#
#
# Del Method Response Headers for 200
#
# Add Response Body for 200 Content type application/json
