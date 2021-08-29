// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function showById(id) {
    let pages = document.getElementsByClassName("page");
    for (page of pages) {
        page.style.display = "none";
    }
    document.getElementById(id).style.display = "block";
}