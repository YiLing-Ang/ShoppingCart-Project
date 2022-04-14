    //let count_decrease = document.getElementsByClassName("count_d");

    //for (let i = 0; i < count_decrease.length; i++) {
    //    count_decrease[i].addEventListener('click', Count_Icre_Decre);
    //    UpdateCartTotal();
    //}

    //let input = document.getElementsByClassName("num_count");

    //for (let i = 0; i < count_decrease.length; i++) {
    //    input[i].addEventListener("keyup", Count_Icre_Decre);
    //}

    //let delete_qty = document.getElementsByClassName("delete_btn");
    //for (let i = 0; i < delete_qty.length; i++) {
    //    delete_qty[i].addEventListener("click", Count_Icre_Decre);
    //}
}

function fallbackCopyTextToClipboard(text) {
    var textArea = document.createElement("textarea");
    textArea.value = text;

    // Avoid scrolling to bottom
    textArea.style.top = "0";
    textArea.style.left = "0";
    textArea.style.position = "fixed";

    document.body.appendChild(textArea);
    textArea.focus();
    textArea.select();

    try {
        var successful = document.execCommand('copy');
        var msg = successful ? 'successful' : 'unsuccessful';
        console.log('Fallback: Copying text command was ' + msg);
    } catch (err) {
        console.error('Fallback: Oops, unable to copy', err);
    }

    document.body.removeChild(textArea);
}



// Main function to copy text to clipboard
function copyTextToClipboard(text) {
    if (!navigator.clipboard) {
        fallbackCopyTextToClipboard(text);
        return;
    }
    navigator.clipboard.writeText(text).then(
        function () {
            console.log('Async: Copying to clipboard was successful!');
        }, function (err) {
            console.error('Async: Could not copy text: ', err);
        }
    );
}