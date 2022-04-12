
window.onload = function () {
    // get all elements with class name "my_img"
    // we tag our images with the class name "my_img" in
    // our razor code
    let elems = document.getElementsByClassName("my_product");

    // add a listener to each of them
    for (let i = 0; i < elems.length; i++) {
        // we want to be notified when this image is being clicked
        elems[i].addEventListener('click', OnMouseClick);
    }

}

function OnMouseClick(event) {
    if (confirm("Confirm add to cart this product?") == true) {
        AddToCart(event.target.id);
    }
}

function AddToCart(id) {
    let xhr = new XMLHttpRequest();

    // sending to our Home controller and we want the
    // DeleteImage action method to receive this
    xhr.open("POST", "/Cart/AddToCart");

    // tells the server our data is a JSON string
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");

    // our server has responded to our request
    xhr.onreadystatechange = function () {
        if (this.readyState == XMLHttpRequest.DONE) {
            if (this.status == 200) {
                // convert server's JSON string to a JavaScript object
                let data = JSON.parse(this.responseText);
                
                // reload the page if success to show the gallery
                // without the just-deleted image
                if (data.addstatus == "success") {
                    cartEle = document.getElementById("cartQtyId");
                    
                    if (cartEle != null) {
                        cartEle.innerHTML = data.cartqty;
                        cartEle.value = data.cartqty;
                    }
                }
            }
        }
    }

    // create a JavaScript object
    let req = {
        ProductId: id
    }

    // serializing to JSON string to send to server
    xhr.send(JSON.stringify(req));
}


