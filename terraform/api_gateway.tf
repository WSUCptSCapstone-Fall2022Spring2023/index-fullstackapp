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

  resource_img = {
    name      = "img"
    path_part = "{folder}"
    child_resource = {
      name      = "{imgname}"
      path_part = "{imgname}"
    }
  }
}

# Create the REST API Gateway
resource "aws_api_gateway_rest_api" "index_rest" {
  name        = "index-rest"
  description = "This API will be used to connect to the s3 bucket called 'index-static-website'."

  binary_media_types = [
    "image/png"
  ]

  endpoint_configuration {
    types = ["REGIONAL"]
  }
}

# Create the resources
resource "aws_api_gateway_resource" "api_resource" {
  for_each = { for r in local.resource_paths : r.name => r }

  rest_api_id = aws_api_gateway_rest_api.index_rest.id
  parent_id   = aws_api_gateway_rest_api.index_rest.root_resource_id
  path_part   = each.key
}

# Configure the /img/{folder}/{imgname} resource
resource "aws_api_gateway_resource" "api_resource_img_folder" {
  rest_api_id = aws_api_gateway_rest_api.index_rest.id
  parent_id   = aws_api_gateway_rest_api.index_rest.root_resource_id
  path_part   = local.resource_img.path_part
}

# Create the img resource
resource "aws_api_gateway_resource" "api_resource_img_name" {
  rest_api_id = aws_api_gateway_rest_api.index_rest.id
  parent_id   = aws_api_gateway_resource.api_resource_img_folder.id
  path_part   = local.resource_img.child_resource.path_part
}

# Add GET method to resources 
resource "aws_api_gateway_method" "api_method" {
  for_each = { for r in local.resource_paths : "${r.name}-GET" => r }

  rest_api_id   = aws_api_gateway_rest_api.index_rest.id
  resource_id   = aws_api_gateway_resource.api_resource[each.value.name].id
  http_method   = "GET"
  authorization = "NONE"

  request_parameters = {
    "method.request.header.X-Api-Key" = true
  }
}

# Add PUT method to resources 
resource "aws_api_gateway_method" "api_method_put" {
  for_each = { for r in local.resource_paths : "${r.name}-PUT" => r }

  rest_api_id   = aws_api_gateway_rest_api.index_rest.id
  resource_id   = aws_api_gateway_resource.api_resource[each.value.name].id
  http_method   = "PUT"
  authorization = "NONE"

  request_parameters = {
    "method.request.header.X-Api-Key" = true
  }
}

# Create the img methods (GET, PUT, DELETE)
resource "aws_api_gateway_method" "api_method_img" {
  for_each = toset(["GET", "PUT", "DELETE"])

  rest_api_id   = aws_api_gateway_rest_api.index_rest.id
  resource_id   = aws_api_gateway_resource.api_resource_img_name.id
  http_method   = each.key
  authorization = "NONE"

  request_parameters = {
    "method.request.header.X-Api-Key" = true
    "method.request.path.folder"      = true
    "method.request.path.imgname"     = true
  }
}

# Define the integration
resource "aws_api_gateway_integration" "api_integration" {
  for_each = { for r in local.resource_paths : "${r.name}-GET" => r }

  rest_api_id             = aws_api_gateway_rest_api.index_rest.id
  resource_id             = aws_api_gateway_resource.api_resource[each.value.name].id
  http_method             = aws_api_gateway_method.api_method[each.key].http_method
  integration_http_method = "GET"
  type                    = "AWS"
  uri                     = "arn:aws:apigateway:${var.region}:s3:path/${aws_s3_bucket.index_static_website.bucket}/${each.value.path_override}"
  credentials             = aws_iam_role.index_api_json_role.arn

  request_templates = {
    "application/json" = <<EOF
{
  "statusCode": 200,
  "headers": {
    "Access-Control-Allow-Origin": "*",
    "Content-Type": "application/json"
  },
  "body" : $input.json('$')
}
EOF
  }

  request_parameters = {
    "integration.request.header.X-Api-Key" = "method.request.header.X-Api-Key"
  }
}

# Define the PUT method RESPONSE
resource "aws_api_gateway_integration" "api_integration_put" {
  for_each = { for r in local.resource_paths : "${r.name}-PUT" => r }

  rest_api_id             = aws_api_gateway_rest_api.index_rest.id
  resource_id             = aws_api_gateway_resource.api_resource[each.value.name].id
  http_method             = aws_api_gateway_method.api_method_put[each.key].http_method
  integration_http_method = "PUT"
  type                    = "AWS"
  uri                     = "arn:aws:apigateway:${var.region}:s3:path/${aws_s3_bucket.index_static_website.bucket}/${each.value.path_override}"
  credentials             = aws_iam_role.index_api_json_role.arn

  request_parameters = {
    "integration.request.header.X-Api-Key" = "method.request.header.X-Api-Key"
  }
}

# Define the integration
resource "aws_api_gateway_integration" "api_integration_img" {
  for_each = toset(["GET", "PUT", "DELETE"])

  rest_api_id             = aws_api_gateway_rest_api.index_rest.id
  resource_id             = aws_api_gateway_resource.api_resource_img_name.id
  http_method             = each.key
  integration_http_method = each.key
  type                    = "AWS"
  uri                     = "arn:aws:apigateway:${var.region}:s3:path/${aws_s3_bucket.index_static_website.bucket}/img/{folder}/{imgname}"
  credentials             = aws_iam_role.index_api_json_role.arn

  request_parameters = {
    "integration.request.header.X-Api-Key" = "method.request.header.X-Api-Key"
    "integration.request.path.folder"      = "method.request.path.folder"
    "integration.request.path.imgname"     = "method.request.path.imgname"
  }

  passthrough_behavior = "WHEN_NO_MATCH" # Add this line
}

# Define the GET method RESPONSE
resource "aws_api_gateway_method_response" "api_method_response" {
  for_each = { for r in local.resource_paths : "${r.name}-GET" => r }

  rest_api_id = aws_api_gateway_rest_api.index_rest.id
  resource_id = aws_api_gateway_resource.api_resource[each.value.name].id
  http_method = aws_api_gateway_method.api_method[each.key].http_method

  status_code = "200"

  response_parameters = {
    "method.response.header.Access-Control-Allow-Origin" = true
    "method.response.header.Content-Type"                = true
  }
}

# image method response
resource "aws_api_gateway_method_response" "api_method_response_img" {
  for_each = toset(["GET", "PUT", "DELETE"])

  rest_api_id = aws_api_gateway_rest_api.index_rest.id
  resource_id = aws_api_gateway_resource.api_resource_img_name.id
  http_method = each.key
  status_code = "200"

  response_models = {
    "application/json" = "Empty"
  }

  depends_on = [
    aws_api_gateway_method.api_method_img
  ]
}

# Define the GET integration RESPONSE
resource "aws_api_gateway_integration_response" "api_integration_response" {
  for_each = { for r in local.resource_paths : "${r.name}-GET" => r }

  rest_api_id       = aws_api_gateway_rest_api.index_rest.id
  resource_id       = aws_api_gateway_resource.api_resource[each.value.name].id
  http_method       = aws_api_gateway_method.api_method[each.key].http_method
  status_code       = "200"
  selection_pattern = ""

  response_parameters = {
  "method.response.header.Access-Control-Allow-Origin" = "'*'"
  "method.response.header.Content-Type"                = "integration.response.header.content-type"
  }

  content_handling = "CONVERT_TO_BINARY"

  depends_on = [
    aws_api_gateway_integration.api_integration
  ]
}

resource "aws_api_gateway_integration_response" "api_integration_response_default" {
  for_each = { for r in local.resource_paths : "${r.name}-GET" => r }

  rest_api_id       = aws_api_gateway_rest_api.index_rest.id
  resource_id       = aws_api_gateway_resource.api_resource[each.value.name].id
  http_method       = aws_api_gateway_method.api_method[each.key].http_method
  status_code       = "200"
  selection_pattern = ".*"

  response_parameters = {
    "method.response.header.Access-Control-Allow-Origin" = "'*'"
  }

  depends_on = [
    aws_api_gateway_integration.api_integration
  ]
}

resource "aws_api_gateway_integration_response" "api_integration_response_img" {
  rest_api_id       = aws_api_gateway_rest_api.index_rest.id
  resource_id       = aws_api_gateway_resource.api_resource_img_name.id
  http_method       = "GET"
  status_code       = "200"
  selection_pattern = ""

  content_handling = "CONVERT_TO_BINARY"

  depends_on = [
    aws_api_gateway_integration.api_integration_img
  ]
}

# Create the IAM role
resource "aws_iam_role" "index_api_json_role" {
  name        = "index-api-json-role"
  description = "This role contains the policy called 'index-api-json-policy'. It is the execution role used for all endpoints of the API Gateway called 'index-rest'."

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
  description = "Allow access to the specified S3 bucket for the API Gateway role."

  policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Action = [
          "s3:GetObject",
          "s3:PutObject",
          "s3:DeleteObject"
        ]
        Effect   = "Allow"
        Resource = "${aws_s3_bucket.index_static_website.arn}/*"
      }
    ]
  })
}

# Attach the required policy to the IAM role
resource "aws_iam_role_policy_attachment" "index_api_json_policy_attachment" {
  policy_arn = aws_iam_policy.index_api_json_policy.arn
  role       = aws_iam_role.index_api_json_role.name
}

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

# Deploy the API
resource "aws_api_gateway_deployment" "index_rest_deployment" {
  rest_api_id = aws_api_gateway_rest_api.index_rest.id
  stage_name  = "Prod"

  variables = {
    "api_key_required" = "true"
  }

  depends_on = [
    aws_api_gateway_method.api_method,
    aws_api_gateway_integration.api_integration,
    aws_api_gateway_method_response.api_method_response,
    aws_api_gateway_integration_response.api_integration_response,
    aws_api_gateway_integration_response.api_integration_response_default
  ]
}

# Create the API Gateway API Key
resource "aws_api_gateway_api_key" "index_rest_api_key" {
  name = "index-rest-api-key"
}

# Associate the API Key with the deployment
resource "aws_api_gateway_usage_plan" "index_rest_usage_plan" {
  name        = "index-rest-usage-plan"
  description = "Usage plan for the index-rest API."

  api_stages {
    api_id = aws_api_gateway_rest_api.index_rest.id
    stage  = aws_api_gateway_deployment.index_rest_deployment.stage_name
  }
}

resource "aws_api_gateway_usage_plan_key" "index_rest_usage_plan_key" {
  key_id        = aws_api_gateway_api_key.index_rest_api_key.id
  key_type      = "API_KEY"
  usage_plan_id = aws_api_gateway_usage_plan.index_rest_usage_plan.id
}