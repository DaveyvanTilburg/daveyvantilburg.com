﻿// Find al rating items
const ratings = document.querySelectorAll(".rating");

// Iterate over all rating items
ratings.forEach((rating) => {
    // Get content and get score as an int
    const ratingContent = rating.innerHTML;
    const ratingScore = parseInt(ratingContent, 10) * 10;

    // Define if the score is good, meh or bad according to its value
    const scoreClass =
        ratingScore < 40 ? "bad" : ratingScore < 60 ? "meh" : "good";

    // Add score class to the rating
    rating.classList.add(scoreClass);

    // After adding the class, get its color
    const ratingColor = window.getComputedStyle(rating).backgroundColor;

    // Define the background gradient according to the score and color
    const gradient = `background: conic-gradient(${ratingColor} ${ratingScore}%, transparent 0 100%)`;

    if (ratingScore >= 90)
        rating.classList.add("favorite");
    
    // Set the gradient as the rating background
    rating.setAttribute("style", gradient);

    // Wrap the content in a tag to show it above the pseudo element that masks the bar
    rating.innerHTML = `<span>${ratingScore / 10} ${ratingContent.indexOf("%") >= 0 ? "<small>%</small>" : ""
        }</span>`;
});
