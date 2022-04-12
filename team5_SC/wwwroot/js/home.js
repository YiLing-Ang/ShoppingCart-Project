let selected = {};
let timer = null;


window.onload = function () {
    /* setup event listeners for tasks selection */
    //let elems = document.getElementsByClassName("a_task");
    let elems = document.getElementsByClassName("btn_add_cart");
    for (let i = 0; i < elems.length; i++) {
        elems[i].addEventListener('click', OnTaskClick);

    }

    /* setup event listener to intercept click on select_count */
    //let elem = document.getElementById("select_count_url");
    //elem.addEventListener('click', OnCountClick);

    // searchbar: Execute a function when the user releases a key on the keyboard
    let searchproduct = document.getElementById("SearchProduct");
    let searchproductval = document.getElementById("SearchProduct").value;

    //  "keyup" - Execute a function when the user releases a key on the keyboard
    searchproduct.addEventListener("keyup", searchitem);


    // load cart quantity if page reload 

    UpdateCartNum();


    let count_increase = document.getElementsByClassName("count_i");


    for (let i = 0; i < count_increase.length; i++) {
        count_increase[i].addEventListener('click', Count_Icre);

    }

}



function OnTaskClick(event) {
    let target = event.currentTarget;


    if (target.id != null) {

        //alert("target.id:" + target.id);
        //document.writeln("target.id:" + target.id);
        let xhr = new XMLHttpRequest();

        xhr.open("POST", "/Cart/AddToCart");
        xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");
        xhr.send(JSON.stringify(target.id));


        xhr.onreadystatechange = function () {
            if (this.readyState == XMLHttpRequest.DONE) {

                if (this.status != 200) {
                    /* error; info user etc */
                    return;
                }
                let data = JSON.parse(this.responseText);

                console.log("data.status:" + data.status);
                UpdateCartNum();
                //document.writeln("data.quantity : " + data.quantity);
            }
        }
        console.log("target.id:" + target.id);
        // if click add to cart button , the cart num will update

        //UpdateCartNum();

    }



}

// add item to cartdetail table by getElementsByClassName("a_product")

function UpdateCartNum() {
    //let select_count;
    console.log("aaaaaaaaaaaaaaa");
    let elem = document.getElementById("select_count");
    //document.writeln("count" + select_count);
    let xhr = new XMLHttpRequest();
    xhr.open("POST", "/Cart/UpdateCart");
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");

    xhr.onreadystatechange = function () {
        if (this.readyState == XMLHttpRequest.DONE) {

            if (this.status != 200) {
                /* error; info user etc */
                return;
            }
            let data = JSON.parse(this.responseText);
            if (data.quantity != 0) {
                //select_count = data.quantity;
                elem.innerHTML = data.quantity;
                console.log("data.quantity : " + data.quantity);
            }
            //document.writeln("data.quantity : " + data.quantity);
        }
    }
    xhr.send();

}

//document.writeln("data.quantity : " + data.quantity);






function searchitem(event) {

    let xhr = new XMLHttpRequest();
    // Number 13 is the "Enter" key on the keyboard
    if (event.keyCode === 13) {
        //alert(document.getElementById("SearchProduct").value);
        event.preventDefault();

        xhr.open("POST", "/Product/AllProducts", true);
        xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");
        xhr.onreadystatechange = function () {
            if (this.readyState == XMLHttpRequest.DONE) {
                //selected = {};

                //after send request , locate this  url
                // https://localhost:5001/Product/AllProducts/?searchStr=easy

                window.location.href = "/Product/AllProducts" + "?searchStr=" + document.getElementById("SearchProduct").value;
            }
        }
        xhr.send();

    }
}



function Count_Icre(event) {
    let target = event.currentTarget;
    console.log("increase count" + target.nodeName);

    var x = this.parentElement.parentElement.getElementById.value;
    console.log("x : idddddd" + x);
    var x1 = this.parentElement.parentElement.nodeName;
    console.log("parentElement.nodeNameidddddd" + x1);
}
