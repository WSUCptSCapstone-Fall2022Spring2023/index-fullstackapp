# Resource names and path overides
locals {
  resource_paths = [
    { name = "events", path_override = "events.json" },
    { name = "meetings", path_override = "meetings.json" },
    { name = "indeximages", path_override = "indexImages.json" },
    { name = "members", path_override = "members.json" },
    { name = "news", path_override = "news.json" },
    { name = "resources", path_override = "resources.json" },
    { name = "specialties", path_override = "specialties.json" },
  ]
}

locals {
  resource_name_to_path = { for item in local.resource_paths : item.name => item.path_override }
}

# Create the REST API Gateway
resource "aws_api_gateway_rest_api" "index_rest" {
  name        = "index-rest"
  description = "This API will be used to connect to the s3 bucket called 'index-static-website'."

  binary_media_types = [
    "image/png",
    "image/jpeg"
  ]

  endpoint_configuration {
    types = ["REGIONAL"]
  }
}




################################### CREATE API RESOURCES ###################################################

# Create the JSON resources
resource "aws_api_gateway_resource" "api_json_resources" {
  for_each    = { for item in local.resource_paths : item.name => item }
  rest_api_id = aws_api_gateway_rest_api.index_rest.id
  parent_id   = aws_api_gateway_rest_api.index_rest.root_resource_id
  path_part   = each.key
}


resource "aws_api_gateway_resource" "api_img_resource" {
  rest_api_id = aws_api_gateway_rest_api.index_rest.id
  parent_id   = aws_api_gateway_rest_api.index_rest.root_resource_id
  path_part   = "img"
}

resource "aws_api_gateway_resource" "api_folder_resource" {
  rest_api_id = aws_api_gateway_rest_api.index_rest.id
  parent_id   = aws_api_gateway_resource.api_img_resource.id
  path_part   = "{folder}"
}

resource "aws_api_gateway_resource" "api_name_resource" {
  rest_api_id = aws_api_gateway_rest_api.index_rest.id
  parent_id   = aws_api_gateway_resource.api_folder_resource.id
  path_part   = "{name}"
}




################################### ADD METHODS TO RESOURCES ###################################################

# Add GET method to JSON resources 
resource "aws_api_gateway_method" "api_json_resources_get" {
  for_each      = aws_api_gateway_resource.api_json_resources
  rest_api_id   = each.value.rest_api_id
  resource_id   = each.value.id
  http_method   = "GET"
  authorization = "NONE"
  api_key_required = true
}

# Add PUT method to JSON resources 
resource "aws_api_gateway_method" "api_json_resources_put" {
  for_each      = aws_api_gateway_resource.api_json_resources
  rest_api_id   = each.value.rest_api_id
  resource_id   = each.value.id
  http_method   = "PUT"
  authorization = "NONE"
  api_key_required = true
}

resource "aws_api_gateway_method" "api_img_resource_get" {
  rest_api_id   = aws_api_gateway_rest_api.index_rest.id
  resource_id   = aws_api_gateway_resource.api_name_resource.id
  http_method   = "GET"
  authorization = "NONE"
  api_key_required = true

    request_parameters = {
    "method.request.path.folder" = true
    "method.request.path.name"   = true
  }
}

resource "aws_api_gateway_method" "api_img_resource_put" {
  rest_api_id   = aws_api_gateway_rest_api.index_rest.id
  resource_id   = aws_api_gateway_resource.api_name_resource.id
  http_method   = "PUT"
  authorization = "NONE"
  api_key_required = true

    request_parameters = {
    "method.request.path.folder" = true
    "method.request.path.name"   = true
  }
}




################################### DEFINE INTEGRATION REQUESTS ###################################################

# JSON GET INTEGRATION REQUESTS
resource "aws_api_gateway_integration" "api_json_resources_get_integration" {
  for_each          = aws_api_gateway_method.api_json_resources_get
  rest_api_id       = each.value.rest_api_id
  resource_id       = each.value.resource_id
  http_method       = each.value.http_method
  integration_http_method = "GET"
  type              = "AWS"
  credentials = aws_iam_role.index_api_json_role.arn

  uri = "arn:aws:apigateway:${var.region}:s3:path/${aws_s3_bucket.index_static_website.bucket}/${local.resource_name_to_path[each.key]}"

  passthrough_behavior = "WHEN_NO_MATCH"
}

# JSON PUT INTEGRATION REQUESTS
resource "aws_api_gateway_integration" "api_json_resources_put_integration" {
  for_each          = aws_api_gateway_method.api_json_resources_put
  rest_api_id       = each.value.rest_api_id
  resource_id       = each.value.resource_id
  http_method       = each.value.http_method
  integration_http_method = "PUT"
  type              = "AWS"
  credentials = aws_iam_role.index_api_json_role.arn
  uri = "arn:aws:apigateway:${var.region}:s3:path/${aws_s3_bucket.index_static_website.bucket}/${local.resource_name_to_path[each.key]}"
  passthrough_behavior = "WHEN_NO_MATCH"
}

# IMAGE GET INTEGRATION REQUESTS
resource "aws_api_gateway_integration" "api_img_resource_get_integration" {
  rest_api_id = aws_api_gateway_rest_api.index_rest.id
  resource_id = aws_api_gateway_resource.api_name_resource.id
  http_method = aws_api_gateway_method.api_img_resource_get.http_method
  type          = "AWS"
  integration_http_method = "GET"
  uri           = "arn:aws:apigateway:${var.region}:s3:path/${aws_s3_bucket.index_static_website.bucket}/img/{folder}/{name}"
  credentials   = aws_iam_role.index_api_json_role.arn
  request_parameters = {
    "integration.request.path.folder" = "method.request.path.folder"
    "integration.request.path.name"   = "method.request.path.name"
  }
}

# IMAGE PUT INTEGRATION REQUESTS
resource "aws_api_gateway_integration" "api_img_resource_put_integration" {
  rest_api_id = aws_api_gateway_rest_api.index_rest.id
  resource_id = aws_api_gateway_resource.api_name_resource.id
  http_method = aws_api_gateway_method.api_img_resource_put.http_method

  type          = "AWS"
  integration_http_method = "PUT"
  uri           = "arn:aws:apigateway:${var.region}:s3:path/${aws_s3_bucket.index_static_website.bucket}/img/{folder}/{name}"
  credentials   = aws_iam_role.index_api_json_role.arn
  request_parameters = {
    "integration.request.path.folder" = "method.request.path.folder"
    "integration.request.path.name"   = "method.request.path.name"
  }
}




###################################  DEFINE METHOD RESPONSES  ###################################################

# JSON GET METHOD RESPONSES
resource "aws_api_gateway_method_response" "api_json_resources_get_response" {
  for_each    = aws_api_gateway_method.api_json_resources_get
  rest_api_id = each.value.rest_api_id
  resource_id = each.value.resource_id
  http_method = each.value.http_method
  status_code = "200"

  response_models = {
    "application/json" = "Empty"
  }
}

# JSON PUT METHOD RESPONSES
resource "aws_api_gateway_method_response" "api_json_resources_put_response" {
  for_each    = aws_api_gateway_method.api_json_resources_put
  rest_api_id = each.value.rest_api_id
  resource_id = each.value.resource_id
  http_method = each.value.http_method
  status_code = "200"

  response_models = {
    "application/json" = "Empty"
  }
}

# IMG GET METHOD RESPONSES
resource "aws_api_gateway_method_response" "api_img_resource_get_method_response" {
  rest_api_id = aws_api_gateway_rest_api.index_rest.id
  resource_id = aws_api_gateway_resource.api_name_resource.id
  http_method = aws_api_gateway_method.api_img_resource_get.http_method
  status_code = "200"

  response_models = {
    "application/json" = "Empty"
  }
}

# IMG PUT METHOD RESPONSES
resource "aws_api_gateway_method_response" "api_img_resource_put_method_response" {
  rest_api_id = aws_api_gateway_rest_api.index_rest.id
  resource_id = aws_api_gateway_resource.api_name_resource.id
  http_method = aws_api_gateway_method.api_img_resource_put.http_method
  status_code = "200"

  response_models = {
    "application/json" = "Empty"
  }
}




###################################  DEFINE INTEGRATION RESPONSES  ###################################################

# JSON GET INTEGRATION RESPONSE
resource "aws_api_gateway_integration_response" "api_json_resources_get_integration_response" {
  for_each    = aws_api_gateway_integration.api_json_resources_get_integration
  rest_api_id = each.value.rest_api_id
  resource_id = each.value.resource_id
  http_method = each.value.http_method
  status_code = "200"

  response_templates = {
    "application/json" = ""
  }

    depends_on = [
    aws_api_gateway_integration.api_json_resources_get_integration,
  ]
}

# JSON PUT INTEGRATION RESPONSE
resource "aws_api_gateway_integration_response" "api_json_resources_put_integration_response" {
  for_each    = aws_api_gateway_integration.api_json_resources_put_integration
  rest_api_id = each.value.rest_api_id
  resource_id = each.value.resource_id
  http_method = each.value.http_method
  status_code = "200"

  response_templates = {
    "application/json" = ""
  }

    depends_on = [
    aws_api_gateway_integration.api_json_resources_put_integration,
  ]
}

# IMG GET INTEGRATION RESPONSE
resource "aws_api_gateway_integration_response" "api_img_resource_get_integration_response" {
  rest_api_id = aws_api_gateway_rest_api.index_rest.id
  resource_id = aws_api_gateway_resource.api_name_resource.id
  http_method = aws_api_gateway_method.api_img_resource_get.http_method
  status_code = aws_api_gateway_method_response.api_img_resource_get_method_response.status_code
  content_handling = "CONVERT_TO_BINARY"

    depends_on = [
    aws_api_gateway_integration.api_img_resource_get_integration,
  ]
}

# IMG PUT INTEGRATION RESPONSE
resource "aws_api_gateway_integration_response" "api_img_resource_put_integration_response" {
  rest_api_id = aws_api_gateway_rest_api.index_rest.id
  resource_id = aws_api_gateway_resource.api_name_resource.id
  http_method = aws_api_gateway_method.api_img_resource_put.http_method
  status_code = aws_api_gateway_method_response.api_img_resource_put_method_response.status_code

    depends_on = [
    aws_api_gateway_integration.api_img_resource_put_integration,
  ]
}




###################################  IAM ROLES AND POLICY FOR S3 GET/PUT ###################################################

# Create the IAM role
resource "aws_iam_role" "index_api_json_role" {
  name = "index-api-json-role"

  assume_role_policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Action = "sts:AssumeRole"
        Effect = "Allow"
        Principal = {
          Service = "apigateway.amazonaws.com"
        }
      }
    ]
  })
}

# Policy to allow API to (GET,PUT,DEL) from s3
resource "aws_iam_policy" "index_api_json_policy" {
  name        = "index-api-json-policy"
  description = "Policy to allow GetObject and PutObject actions for the S3 bucket"

  policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Action = [
          "s3:GetObject",
          "s3:PutObject"
        ]
        Effect   = "Allow"
        Resource = "arn:aws:s3:::${aws_s3_bucket.index_static_website.bucket}/*"
      }
    ]
  })
}

# Attach the required policy to the IAM role
resource "aws_iam_role_policy_attachment" "index_api_json_policy_attachment" {
  policy_arn = aws_iam_policy.index_api_json_policy.arn
  role       = aws_iam_role.index_api_json_role.name
}



###################################  IAM ROLES AND POLICY FOR API CLOUDWATCH ###################################################

#Create the IAM role for API Gateway to create cloudwatch logs for testing and debugging:
resource "aws_iam_role" "api_gateway_cloudwatch_logs_role" {
  name = "ApiGatewayCloudWatchLogsRole"

  assume_role_policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Action = "sts:AssumeRole"
        Effect = "Allow"
        Principal = {
          Service = "apigateway.amazonaws.com"
        }
      }
    ]
  })
}

# Attach the "AmazonAPIGatewayPushToCloudWatchLogs" policy to the IAM role:
resource "aws_iam_role_policy_attachment" "api_gateway_cloudwatch_logs_policy_attachment" {
  policy_arn = "arn:aws:iam::aws:policy/service-role/AmazonAPIGatewayPushToCloudWatchLogs"
  role       = aws_iam_role.api_gateway_cloudwatch_logs_role.name
}

# Set the CloudWatch Logs role ARN in API Gateway account settings:
resource "aws_api_gateway_account" "api_gateway_account_settings" {
  provider = aws.apigateway_account
  cloudwatch_role_arn = aws_iam_role.api_gateway_cloudwatch_logs_role.arn
}



###################################  DEPLOY API WITH API KEY ###################################################

# Deploy the API
resource "aws_api_gateway_deployment" "api_deployment" {
  depends_on  = [aws_api_gateway_integration.api_json_resources_get_integration, aws_api_gateway_integration.api_json_resources_put_integration]
  rest_api_id = aws_api_gateway_rest_api.index_rest.id
  stage_name  = "prod"
}

# Create API usage plan
resource "aws_api_gateway_usage_plan" "api_usage_plan" {
  name        = "index-api-usage-plan"
  description = "Usage plan for the Index API"

  api_stages {
    api_id = aws_api_gateway_rest_api.index_rest.id
    stage  = aws_api_gateway_deployment.api_deployment.stage_name
  }
}

# create API key
resource "aws_api_gateway_api_key" "api_key" {
  name = "index-api-key"
}

# Associate the API Key with the deployment
resource "aws_api_gateway_usage_plan_key" "api_usage_plan_key" {
  key_id        = aws_api_gateway_api_key.api_key.id
  key_type      = "API_KEY"
  usage_plan_id = aws_api_gateway_usage_plan.api_usage_plan.id
}