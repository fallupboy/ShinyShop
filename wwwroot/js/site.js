$(function () {
    $.ajax({
        type: "GET",
        url: "/NFT/getIndexData",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (chapters) {
            $.each(chapters, function () {
                let column = document.createElement("div");
                column.id = "imgColumn" + this.id;
                column.className = "column";
                column.style.float = "left";
                column.style.width = "20%";
                column.style.marginBottom = "30px";
                column.style.textAlign = "center";

                let img = document.createElement("img");
                img.src = this.imageData;
                img.id = "nftImage" + this.id;
                img.style.width = "80%";
                img.style.padding = "2px";
                img.style.border = "5px solid #000";

                let imgNameDiv = document.createElement("div");
                imgNameDiv.id = "imgNameDiv" + this.id;
                imgNameDiv.style.padding = "5px";

                var imgName = '<a class="btn btn-primary" asp-action="Details" asp-controller="NFT" asp-route-id="' + this.id + '" style="font-size: 15px;">' + this.imageName + ' | ' + this.id + '</a>';

                document.getElementById("imgRow").appendChild(column);
                document.getElementById("imgColumn" + this.id).appendChild(img);
                document.getElementById("imgColumn" + this.id).appendChild(imgNameDiv);
                document.getElementById("imgNameDiv" + this.id).insertAdjacentHTML('afterbegin', imgName);
            });
        }
    });
});

$(document).ready(function () {
    $("#myInput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $(".row .column").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});
