var Zegar 
= 
{    

    init: function () 
    {
        //utworzenie i zastartowanie zegara
        Zegar.makeZegar();
        setInterval(Zegar.makeZegar, 1000);
        setInterval(function()
        {
            if($("#zegarSrodek").html()=="&nbsp;")
            {
                $("#zegarSrodek").html(":") 
                $("#zegarGodzina").css("right","0")
                $("#zegarMinuty").css("left","0")
            }     
            else
            {
                $("#zegarSrodek").html("&nbsp;") 
                $("#zegarGodzina").css("right","-0.05vw")
                $("#zegarMinuty").css("left","-0.05vw")
            }         
        }, 1000);
    },

    makeZegar: function()
    {
        var dzisiaj=new Date()
        $("#zegarGodzina").html(dzisiaj.getHours())
        $("#zegarMinuty").html(dzisiaj.getMinutes())
        if($("#zegarMinuty").html().length==1)
        $("#zegarMinuty").html("0"+$("#zegarMinuty").html())
        
        //konstrukcja zegara
    }

}