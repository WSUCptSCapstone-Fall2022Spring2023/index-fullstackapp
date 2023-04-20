terraform {
  backend "s3" {
    bucket         = "index-terraform-state"
    key            = "terraform.tfstate"
    region         = "us-east-1"
    dynamodb_table = "index-terraform-lock-table"
    encrypt        = true
  }
}

resource "aws_s3_bucket" "terraform_state" {
  bucket = "index-terraform-state"
  acl    = "private"

  versioning {
    enabled = true
  }

  tags = {
    Terraform   = "true"
    Environment = "dev"
  }
}

resource "aws_dynamodb_table" "terraform_lock" {
  name           = "index-terraform-lock-table"
  billing_mode   = "PAY_PER_REQUEST"
  hash_key       = "LockID"

  attribute {
    name = "LockID"
    type = "S"
  }

  tags = {
    Terraform   = "true"
    Environment = "dev"
  }
}