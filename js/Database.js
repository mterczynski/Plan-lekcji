var Database = {

    methods:
    {
        createTables: function (obj)
        {
            return Ajax.send(obj, Settings.urls.databaseUrl);
        },
        downloadData: function (obj) {
            return Ajax.send(obj, Settings.urls.downloadUrl);
        },
        updateTables: function (obj)
        {
            return Ajax.send(obj, Settings.urls.updateUrl);
        }
    }
}

