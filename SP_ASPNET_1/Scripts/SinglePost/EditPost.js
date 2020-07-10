
        $("#btnSave").click(function (e) {

            var formData = $("#editForm").serialize();

            $.ajax({
        url: "/Blog/EditBlog",
                type: "POST",
                dataType: 'json',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                data: formData,
                success: function (response) {
                    if (response.success) {
        alert(response.responseText);
                    } else {
        alert(response.responseText);
                    }
                },
                error: function (response) {
        alert("error!");
                }

            });
        });

