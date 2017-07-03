var Ajax = {
    send: function (obj, url, dataType) 
    {
        return $.ajax
        ({
            type: "POST",
            url: url,
            data: obj,
            dataType: dataType,
        })
    }
}