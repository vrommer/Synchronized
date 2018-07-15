function ajaxRequest(method, url, data) {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: method,
                url: url,
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                success: data => {
                    resolve(data);
                },

                statusCode: {
                    400: data => {
                        reject(data);
                    }
                },

                failure: data => {
                    console.log("DEBUG: Failure!")
                },

                error: data => {
                    console.log("DEBUG: Error!")
                }
            });
        });
    }