provider "aws" {
  alias  = "apigateway_account"
  region = "us-east-1"
}

variable "region" {
  description = "AWS region"
  default     = "us-east-1"
}

#aws s3 rm s3://index-static-website --recursive