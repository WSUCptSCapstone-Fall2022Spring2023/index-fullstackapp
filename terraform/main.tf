provider "aws" {
  alias  = "apigateway_account"
  region = "us-east-1"
}

variable "region" {
  description = "AWS region"
  default     = "us-east-1"
}