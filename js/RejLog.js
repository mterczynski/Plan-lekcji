var RejLog=
{
    refresh:function()
    {
        if ($("#RejLog"))
            $("#RejLog").remove();
        var RejLogObject = $("<div id='RejLog'></div>");
        $("#container").append(RejLogObject)
        RejLogObject.append("<span id='RejLogHeader'>Rejestracja i logowanie</span>")
        RejLogObject.append("<div id='RejLogLogin'> <span>login</span> <input type='text'> </div>")
        RejLogObject.append("<div id='RejLogPassword'> <span>hasło</span> <input type='password'> </div>")
        RejLogObject.append("<span id='RejLogConfirm'>Dalej</span>")
    }
}