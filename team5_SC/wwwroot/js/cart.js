window.onload = function () {
    let count_increase = document.getElementsByClassName("count_i");

    for (let i = 0; i < count_increase.length; i++) {
        count_increase[i].addEventListener('click', Count_Icre_Decre);
        UpdateCartTotal();
    }

    let count_decrease = document.getElementsByClassName("count_d");

    for (let i = 0; i < count_decrease.length; i++) {
        count_decrease[i].addEventListener('click', Count_Icre_Decre);
        UpdateCartTotal();
    }

    let input = document.getElementsByClassName("num_count");

    for (let i = 0; i < count_decrease.length; i++) {
        input[i].addEventListener("keyup", Count_Icre_Decre);
    }

    let delete_qty = document.getElementsByClassName("delete_btn");
    for (let i = 0; i < delete_qty.length; i++) {
        delete_qty[i].addEventListener("click", Count_Icre_Decre);
    }
}

function Count_Icre_Decre(event) {

    let target = event.currentTarget;
    console.log("increase count" + target.className);

    console.log("parentElement.nodeName product id" + target.parentNode.id);

    if (target.parentNode.id != null) {

        //let target = event.currentTarget;
        //console.log("increase count" + target.className);

        //console.log("parentElement.nodeName product id" + target.parentNode.id);

        let xhr = new XMLHttpRequest();

        xhr.open("POST", "/Cart/CountIcreOrDcre");
        xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");

        if (target.className === "num_count" && event.keyCode === 13) {
            console.log("num_count count" + target.className);
            console.log("num_count count" + target.value);
            let CartUpdate = {
                ClassName: target.className,
                ItemQuantity: target.value,
                ProductId: target.parentNode.id,
            };
            xhr.send(JSON.stringify(CartUpdate));
        } else if (target.className === "count_i" || target.className === "count_d" || target.className === "delete_btn") {
            console.log("incre count" + target.className);
            let CartUpdate = {
                ClassName: target.className,
                ProductId: target.parentNode.id
            };
            xhr.send(JSON.stringify(CartUpdate));
        }
        //else if (target.className === "delete_btn") {
        //    console.log("delete_btn count" + target.className);
        //    let CartUpdate = {
        //        ClassName: target.className,
        //        ProductId: target.parentNode.id
        //    };
        //    xhr.send(JSON.stringify(CartUpdate));
        //}

        xhr.onreadystatechange = function () {
            if (this.readyState == XMLHttpRequest.DONE) {

                if (this.status != 200) {
                    /* error; info user etc */
                    return;
                }
                let data = JSON.parse(this.responseText);

                console.log("data.status:" + data.status);

                if (data.status == "success") { location.reload(); }
            }
        }

        console.log("complete count quantity");
    }
}

function UpdateCartTotal() {

    let elem = document.getElementById("cart_total");
    //if (count_increase != null || ) {

    //}
    let xhr = new XMLHttpRequest();

    xhr.open("POST", "/Cart/UpdateCartTotalAmount");
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");

    xhr.send();
    xhr.onreadystatechange = function () {
        if (this.readyState == XMLHttpRequest.DONE) {

            if (this.status != 200) {
                /* error; info user etc */
                return;
            }
            let data = JSON.parse(this.responseText);

            console.log("data.status:" + data.status);
            elem.innerHTML = data.carttotal;
        }
    }
}