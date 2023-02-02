# Sprint 4 Report (1/9/23 - 2/2/2023)
 * Website "about us" page
 * Website "Newsletter" page
 * Editor app "about us" tab
 * Updates/additions to API
 * A newletter signup component on website (unfinished, visual only)
 * News page
 * Specialties Json and infrastructure added

## Work Summary (Developer Facing)
In this sprint the first and primary objective was adjusting for a more modular approach.
This required a lot of brainstorming, trail and error and refactoring. These changes 
primarily took place in the editor app and the json files. Planning for and contructing
new json object for as much of the website as possible. After planning and refactoring,
we were able to complete the "about us" page on with the website and editor app. We also worked a little bit on the newsletter page with dynamically add/remove the news from the website via the Editor app.

## Unfinished Work
 * newsletter functionality
The newsletter template has been made and we are currently reasearching for a suitable plugin.
 * Editor app news page tab
The editor app news page was postponed due to the unforseen necessity of
creating the "specialties" functionality which was required to complete the "about us" tab.

## Completed Issues/User Stories
Here are links to the issues that we completed in this sprint:
 * [About Us page](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/issues/41)
 * [About Us Page Editor-App](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/issues/43)
 * [Sprint 4 Report](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/issues/48)


 ## Incomplete Issues/User Stories
 Here are links to issues we worked on but did not complete in this sprint:
 * [Editor app news page tab](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/issues/44)
Additional work was required for about tab and pushed this deadline to next sprint.

## Code Files for Review
Please review the following code files, which were actively developed during this sprint, for quality:
### Editor App Backend
 * [Members.cs](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/blob/main/index-editor-app/index-editor-app/Members.cs)
 * [MembersHandler.cs](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/blob/main/index-editor-app/index-editor-app-engine/MembersHandler.cs)
 * [MembersPage.cs](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/blob/main/index-editor-app/index-editor-app-engine/JsonClasses/MembersPage.cs)
 * [reateSpecialtyForm.cs](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/blob/main/index-editor-app/index-editor-app/CreateSpecialtyForm.cs)
 * [IndexAPIClient.cs](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/blob/main/index-editor-app/index-editor-app-engine/IndexAPIClient.cs)
### Website Frontend
 * [AboutUs.vue](https://github.com/WSUCptSCapstone-Fall2022Spring2023/index-fullstackapp/blob/main/front-end/index-vue/src/components/AboutUs.vue)

## Retrospective Summary
Here's what went well:
  * Refactoring the editor-app to handle a wider variety of pages
  * Creation of new json files for website pages
  * Adding new endpoints to API
 
Here's what we'd like to improve:
   * Add additional validation to about us tab in the aditor app
   * improve editor app UI aesthetics.
  
Here are changes we plan to implement in the next sprint:
   * Add news tab to editor app
   * Add indiviual board member pages
   * Add functionality to newletter