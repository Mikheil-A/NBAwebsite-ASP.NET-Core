var index = 0;
var x = document.getElementsByClassName("textImages");
function displayTextImage() {
    x[index].style.display = "inline";
    for (var i = 0; i < x.length; i++) {
        if (i == index)
            continue;
        x[i].style.display = "none";
    }
}

function changetheTextImage() {
    index++;
    if (index == x.length)
        index = 0;
    displayTextImage();
}

function isTextValid() {
    var x = document.getElementById("pictureText").value;
    if (x == "flextime" || x == "ncernsib" ||
        x == "6Gpz8IH" || x == "entsTo")
        return true;
    return false;
}

/*Submitting the form*/
function formSubmited() {
    if (isTextValid())
        return alert("The form has submitted successfully!");

    var x = document.getElementById("pictureText");
    alert("The robot validation text is invalid!");
    x.style.backgroundColor = "red";
}

window.addEventListener("load", displayTextImage);