# s3 sub-folder names
locals {
  img_sub_folders = [
    "eventimages",
    "memberimages",
    "newsimages",
    "resourceimages",
    "specialtyimages"
  ]
}

# Create the S3 bucket
resource "aws_s3_bucket" "index_static_website" {
  bucket = "index-static-website"

  tags = {
    Name        = "index-static-website"
    Environment = "Dev"
  }
}

# Add /img to root
resource "aws_s3_bucket_object" "img_folder" {
  bucket  = aws_s3_bucket.index_static_website.id
  key     = "img/"
  content = ""
}

# Add /img to root
resource "aws_s3_bucket_object" "css_folder" {
  bucket  = aws_s3_bucket.index_static_website.id
  key     = "css/"
  content = ""
}

# Add /img to root
resource "aws_s3_bucket_object" "js_folder" {
  bucket  = aws_s3_bucket.index_static_website.id
  key     = "js/"
  content = ""
}

# Add sub-folders to /img
resource "aws_s3_bucket_object" "img_sub_folders" {
  for_each = toset(local.img_sub_folders)

  bucket  = aws_s3_bucket.index_static_website.id
  key     = "img/${each.value}/"
  content = ""
}

# Set the ACL of the s3 bucket "index_static_website" to public-read
resource "aws_s3_bucket_acl" "index_static_website_acl" {
  bucket = aws_s3_bucket.index_static_website.id
  acl    = "public-read"
}

# set the website configuration for the index_static_website S3 bucket
resource "aws_s3_bucket_website_configuration" "index_static_website" {
  bucket = aws_s3_bucket.index_static_website.id

  # Default index document for the website
  index_document {
    suffix = "index.html"
  }

  # Custom error document that will be displayed when an error occurs
  error_document {
    key = "index.html"
  }
}

# Bucket policy for public access
resource "aws_s3_bucket_policy" "index_static_website_policy" {
  bucket = aws_s3_bucket.index_static_website.id

  policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Action    = "s3:GetObject"
        Effect    = "Allow"
        Resource  = "${aws_s3_bucket.index_static_website.arn}/*"
        Principal = "*"
      }
    ]
  })
}

