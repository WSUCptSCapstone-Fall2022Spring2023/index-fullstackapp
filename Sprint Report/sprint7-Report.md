# Sprint 7 Report (4/2/2023 - 5/2/2023)

## What's New (User Facing)
 * Fixed terraform bugs for Post methods/integrations
 * Updated workflow to deploy AWS resources
 * Fixed bugs with saving and loading editor app backups.
 * Added remote state to terraform.
 * Added branches for contact page.

## Work Summary (Developer Facing)
In this sprint we placed a focus on preparing the project to be taken over. This included finalizing the terraform code for creating the AWS infrastructure. There were
a few minor buug fixes with the creation of the Post methods and integrations. Also, a new workflow was created for deploying infrastucture on push which required 
additional AWS resources to store the terraform state in a remote location. Finally, some more validation was included in the editor in the form of Json Schemas.

## Unfinished Work

## Completed Issues/User Stories
Here are links to the issues that we completed in this sprint:
 * [#92 Fix Save and Load on Editor-app](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/issues/92)
 * [#93 Update workflow to deploy AWS resources](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/issues/93)
 * [#94 Fix terraform API Post methods/integrations](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/issues/94)
 * [#95 Add Json schema validation to Editor-app](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/issues/95)
 * [#81 Add Contact page for different branches](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/issues/81)

 Reminders (Remove this section when you save the file):
  * Each issue should be assigned to a milestone
  * Each completed issue should be assigned to a pull request
  * Each completed pull request should include a link to a "Before and After" video
  * All team members who contributed to the issue should be assigned to it on GitHub
  * Each issue should be assigned story points using a label
  * Story points contribution of each team member should be indicated in a comment
 
## Incomplete Issues/User Stories

## Code Files for Review
Please review the following code files, which were actively developed during this sprint, for quality:
 * [api_gateway.tf](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/blob/main/terraform/api_gateway.tf)
 * [remote_state.tf](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/blob/main/terraform/remote_state.tf)
 * [workflow.yml](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/blob/main/.github/workflows/workflow.yml)
 * [SaveLoad.cs](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/blob/main/index-editor-app/index-editor-app-engine/SaveLoad.cs)
 * [JsonSchemas](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/tree/main/index-editor-app/index-editor-app-engine/JsonSchemas)
 * [Contact.vue](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/blob/main/front-end/index-vue/src/components/Contact.vue)
 * [Contacts.vue](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/blob/main/front-end/index-vue/src/components/Contacts.vue)

## Retrospective Summary
Here's what went well:
  * Save/Load bug fix
  * Terraform API Post method bug fixes
  * Adding schema validation to editor-app
  * Branches for contact page
 
Here's what we'd like to improve:
   * AWS IAM required for github terraform deployments