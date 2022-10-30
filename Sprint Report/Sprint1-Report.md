# Sprint x Report (8/26/21 - 9/24/2021)

## What's New (User Facing)
 * Navigation bar to redirect to Home, About us, Get Involved, Contact, Resources, Services and Events

## Work Summary (Developer Facing)
For Sprint 1, we spent most of the time working on documentation (team inventory, project description, requirement and specification, solution approach) and try to be in touch with the client. We had a hard time catching up with the documentation since we couldn't effectively communicate with the client but we finally caught up. We have set up AWS resources, workflows, and have some back-end functionality. Currently hosted at http://index-webapp.s3-website-us-east-1.amazonaws.com/ the website displays an events.json. The editor app currently allows you to get, edit and send raw json to our aws s3 bucket through the use of https and an API gateway.

## Unfinished Work
N/A

## Completed Issues/User Stories
Here are links to the issues that we completed in this sprint:

 * [#1 Team Inventory](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/issues/1)
 * [#2 Project Description section](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/issues/2)
 * [#3 Requirements and Specifications section](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/issues/3)
 * [#4 Solution Approach section](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/issues/4)
 * [#5 Sprint 1](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/issues/5)
 * [#11 Editor app API](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/issues/11): video included in pull request comment
 * [#10 Display events](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/issues/10): video included in pull request comment
 
 ## Incomplete Issues/User Stories
 Here are links to issues we worked on but did not complete in this sprint:
 N/A

## Code Files for Review
Please review the following code files, which were actively developed during this sprint, for quality:
 * [HelloWorld.vue](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/blob/main/front-end/index-vue/src/components/HelloWorld.vue)
 * [EventsHandler.cs](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/blob/main/index-editor-app/index-editor-app-engine/EventsHandler.cs)
 * [IndexAPIClient.cs](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/blob/main/index-editor-app/index-editor-app-engine/IndexAPIClient.cs)
 * [Form1.cs ](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/blob/main/index-editor-app/index-editor-app/Form1.cs)
 
## Retrospective Summary
Here's what went well:
  * We did a good job setting up AWS resources (s3, API, roles and policies)
  * Workflow to deploy website had no issues
  * We did a good job on the quality of documentation
 
Here's what we'd like to improve:
   * We were very late with the assignments, we want to get all of the documents done in time
   * We had a few problems with braching/merging on Github since fews of us are not familiar with it yet, we'd like to get to know it more and be comfortable with it as we go.
   * Assigning issues to pull requests
   
Here are changes we plan to implement in the next sprint:
   * Complete UI for the website's Homepage.
   * Complete UI for events tab in editor app
   * Uploading event images from editor app
   * Add router to vue (sync URLs to views)
