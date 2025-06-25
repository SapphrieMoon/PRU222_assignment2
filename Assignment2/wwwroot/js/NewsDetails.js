"use strict";

const newsConnection = new signalR.HubConnectionBuilder()
    .withUrl("/newsHub")
    .build();

newsConnection.on("NewsArticleUpdated", function () {
    // Reload the page to get the latest data
    location.reload();
});
newsConnection.on("NewsArticleDeleted", function () {
    // Redirect to the list page if the article is deleted
    window.location.href = '/NewsArticle/Index';
});

newsConnection.start().catch(function (err) {
    return console.error(err.toString());
}); 