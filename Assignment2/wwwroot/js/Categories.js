// Categories.js - SignalR real-time for categories
const categoryConnection = new signalR.HubConnectionBuilder()
    .withUrl("/categoryHub")
    .build();

categoryConnection.on("CategoryCreated", function (category) {
    // Handle real-time category creation (e.g., update UI)
    console.log("Category created:", category);
    // TODO: Add your UI update logic here
});

categoryConnection.on("CategoryAdded", function (category) {
    // Handle real-time category addition (if different from create)
    console.log("Category added:", category);
    // TODO: Add your UI update logic here
});

categoryConnection.start().catch(function (err) {
    return console.error(err.toString());
});
