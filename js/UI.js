var UI 
= 
{
    init: function () 
    {
        WygladMenu:
        {
            function DodanieMouseover()
            {
                $("#listaMainGfx1").mouseover(function()
                {
                    $(this).css("background-color", "rgb(51,75,119)") 
                    $("#listaMainNapis1").css("background-color", "rgb(70,106,168)")
                })
                $("#listaMainGfx2").mouseover(function()
                {
                    $(this).css("background-color", "rgb(51,75,119)") 
                    $("#listaMainNapis2").css("background-color", "rgb(70,106,168)")
                })
                $("#listaMainGfx3").mouseover(function()
                {
                    $(this).css("background-color", "rgb(51,75,119)")
                    $("#listaMainNapis3").css("background-color", "rgb(70,106,168)")
                })
                $("#listaMainNapis1").mouseover(function()
                {
                    $(this).css("background-color", "rgb(70,106,168)")  
                    $("#listaMainGfx1").css("background-color", "rgb(51,75,119)") 
                })
                $("#listaMainNapis2").mouseover(function()
                {
                    $(this).css("background-color", "rgb(70,106,168)") 
                    $("#listaMainGfx2").css("background-color", "rgb(51,75,119)") 
                })
                $("#listaMainNapis3").mouseover(function()
                {
                    $(this).css("background-color", "rgb(70,106,168)") 
                    $("#listaMainGfx3").css("background-color", "rgb(51,75,119)") 
                })
            }
            function DodanieMouseout()
            {
                $("#listaMainGfx1").mouseout(function()
                {
                    $(this).css("background-color", "black")  
                    $("#listaMainNapis1").css("background-color", "#111111")
                })
                $("#listaMainGfx2").mouseout(function()
                {
                    $(this).css("background-color", "black")
                    $("#listaMainNapis2").css("background-color", "#111111")
                })
                $("#listaMainGfx3").mouseout(function()
                {
                    $(this).css("background-color", "black") 
                    $("#listaMainNapis3").css("background-color", "#111111")
                })
                $("#listaMainNapis1").mouseout(function()
                {
                    $(this).css("background-color", "#111111") 
                    $("#listaMainGfx1").css("background-color", "black")  
                })
                $("#listaMainNapis2").mouseout(function()
                {
                    $(this).css("background-color", "#111111")
                    $("#listaMainGfx2").css("background-color", "black")  
                })
                $("#listaMainNapis3").mouseout(function()
                {
                    $(this).css("background-color", "#111111") 
                    $("#listaMainGfx3").css("background-color", "black")  
                })
            }
            DodanieMouseover()
            DodanieMouseout()  
        }
        
      
    },
    methods:
    {
        showAlert: function (text) 
        {
            $(".AlertStaticMessage").html(text)
            $(".AlertStatic").animate({top: "0"},"fast")
        },
        closeAlert: function () 
        {
            $(".AlertStatic").animate({top: "-100%"},"fast")
        },
        showAlertLoading: function (text) 
        {
            $(".AlertLoading b").html(text)
            $(".AlertLoading").animate({top: "0"},"fast")
        },
        closeAlertLoading: function () 
        {
            $(".AlertLoading").animate({top: "-100%"},"fast")
        },
    }  
}
