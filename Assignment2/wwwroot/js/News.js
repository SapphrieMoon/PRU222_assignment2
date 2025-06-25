"use strict";

const newsConnection = new signalR.HubConnectionBuilder()
    .withUrl("/newsHub")
    .build();

newsConnection.on("NewsArticleCreated", function () {
    location.reload();
});

newsConnection.on("NewsArticleUpdated", function () {
    location.reload();
});

newsConnection.on("NewsArticleDeleted", function () {
    location.reload();
});

newsConnection.start().catch(function (err) {
    return console.error(err.toString());
}); 