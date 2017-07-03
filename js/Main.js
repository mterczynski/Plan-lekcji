var Main = {

    init: function () {
		//np inicjalizacja wyglądu elementów interfejsu
		//poszczególnych ekranów
        function DodanieOnclickNaElementachListy()
        {
            $("#listaMainGfx1").click(function(){
                $("#Ustawienia").animate({ left: "0%" }, 'fast')
            })
            $("#listaMainNapis1").click(function(){
                $("#Ustawienia").animate({ left: "0%" }, 'fast')
            })
            $("#listaMainGfx2").click(function(){
                $("#Dzisiaj").animate({ left: "0%" }, 'fast')
            })
            $("#listaMainNapis2").click(function(){
                $("#Dzisiaj").animate({ left: "0%" }, 'fast')
            })
            $("#listaMainGfx3").click(function(){
                $("#Tydzien").animate({ left: "0%" }, 'fast')
                $("#TydzienLeftIcon").css("z-index","6")
            })
            $("#listaMainNapis3").click(function(){
                $("#Tydzien").animate({ left: "0%" }, 'fast')
                $("#TydzienLeftIcon").css("z-index", "6")
            })
            $("#Ustawienia_database").click(function () {
                $("#BazaDanych").animate({ left: "0%" }, 'fast')
                $("#Ustawienia").animate({ left: "-100%" }, 'fast')
            })
            $("#ustawieniaLiGodziny").click(function ()
            {
                $("#Godziny").animate({ left: "0%" }, 'fast')
            })
        }
        function DodanieWyjsciaZekranow()
        {
            $("#Ustawienia").click(function(e)
            {
                if (e.target !== this)
                return
                $(this).animate({ left: "-100%" }, 'fast')
            })

            $("#Godziny").click(function (e) {
                if (e.target !== this)
                    return
                $(this).animate({ left: "-100%" }, 'fast')
            })
            $("#Dzisiaj").click(function (e) {
                if (e.target !== this)
                    return
                $(this).animate({ left: "-100%" }, 'fast')
            })
            $("#Tydzien").click(function(e)
            {
                if (e.target !== this)
                    return
                $(this).animate({ left: "-100%" }, 'fast')
                $("#TydzienLeftIcon").css("z-index", "-6")
            })
            $("#BazaDanych").click(function (e) {
                if (e.target !== this)
                    return
                $(this).animate({ left: "-100%" }, 'fast')
                $("#Ustawienia").animate({ left: "-00%" }, 'fast')
            })
            $(".AlertStatic").click(function () {
                $(this).animate({ top: "-100%" }, 'fast')
            })
        }
        function RozwijanieMenu()
        {
            $("#imgMenu").click(function()
            {
                if($(this).css("opacity")==1)
                {
                   var bottom=$("#listaMain").css("bottom")
                    if(bottom.charAt(bottom.length-1)=="x")
                        bottom=bottom.slice(0,-2)
                    if(bottom==0)
                    {
                        $("#listaMain").animate(
                        {
                           bottom:"-100%"
                        },300)
                    }
                    else
                    {
                        $("#listaMain").animate(
                        {
                           bottom:0
                        },300)
                    }
                }
            })
        }
        $("#BazaDanych ul li").click(function ()
        {

            var obj = {action: ""}
            var ktory = 0;
            switch ($(this).index()) {
                case 0:{
                    obj.action = "CREATE"
                    ktory = 0;
                    break;
                }
                case 1:{
                    obj.action = "DROP"
                    ktory = 1;
                    break;
                }
                case 2:{
                    obj.action = "INSERT"
                    ktory = 2;
                    break;
                }
                case 3:{
                    obj.action = "DELETE"
                    ktory = 3;
                    break;
                }
                case 4: {
                    obj.action = "SELECT"
                    ktory = 4;
                    break;
                }
            }
            UI.methods.showAlertLoading("Proszę czekać")
            if(ktory<4)
                Database.methods.createTables(obj)
                .done(function (response) {
                    UI.methods.closeAlertLoading(UI.methods.showAlert(response))
                })
                .fail(function (response) {
                    UI.methods.closeAlertLoading(UI.methods.showAlert(response))
                })
            else if(ktory==4)
                Database.methods.downloadData(obj)
                .done(function (response)
                {
                    UI.methods.closeAlertLoading(UI.methods.showAlert(response))
                    if(ktory==4)
                    {
                        
                        dodanieGodzin:
                        {
                            var parsed = JSON.parse(response).godziny;
                            godziny = [];
                            for (var x in parsed) {
                                godziny.push(parsed);
                            }
                            godziny = godziny[0];
                            $("#Godziny").html("");
                            $("#Godziny").append("<table></table>");
                            $("#Godziny table").append("<tr><td>Numer lekcji</td><td colspan='2'>Od godziny</td><td colspan='2'>do godziny</td></tr>")
                            for (var i = 0; i < godziny.length; i++) {
                                var tr = $("<tr></tr>")
                                //dodanie zer
                                var minuty = godziny[i].odM;
                                if (minuty === "0" || minuty === 0 || minuty === 5 || minuty === "5")
                                    minuty = "0" + minuty;
                                //koniec dodawania zer
                                var td = $("<td>" + godziny[i].id + "</td>")
                                tr.append(td)
                                var td = $("<td colspan='2'>" + godziny[i].odG + ":" + minuty + "</td>")
                                tr.append(td);
                                //dodanie zer
                                minuty = godziny[i].doM;
                                if (minuty === "0" || minuty === 0 || minuty === 5 || minuty === "5")
                                    minuty = "0" + minuty;
                                //koniec dodawania zer
                                var td = $("<td colspan='2'>" + godziny[i].doG + ":" + minuty + "</td>")
                                tr.append(td)
                                $("#Godziny table").append(tr)
                            }
                            $("#Godziny > table > tr > td").click(function () {
                                if ($(this).parent().index() > 0) {
                                    myObj = {
                                        action: "",
                                        column: "",
                                        row: "",
                                        hours: "",
                                        minutes: "",
                                    }
                                    myObj = {
                                        action: "UPDATE",
                                        column: $(this).index(),
                                        row: $(this).parent().index(),
                                        hours: "",
                                        minutes: "",
                                    }
                                    $("#WyborGodziny").animate({ left: "0%" }, 'fast')
                                }
                            })
                        }
                        dodanieDzisiaj:
                        {
                            var parsed = JSON.parse(response).dzisiaj
                            console.log(parsed.length)
                            $("#Dzisiaj").html("");
                            $("#Dzisiaj").append("<div id='DzisiajLeft'> <img alt='' id='DzisiajGradient' src='gfx/gradientBlue1.png'> <img alt='' id='DzisiajLeftIcon' src='gfx/top.svg'> <div>");
                            $("#Dzisiaj").append("<table></table>");
                            $("#Dzisiaj table").append("<tr><td>Numer lekcji</td><td>Nazwa przedmiotu</td><td>Numer sali</td></tr>")
                            for (var i = 0; i < parsed.length; i++)
                            {
                                var tr = $("<tr></tr>")
                                var id = i+1;
                                var przedmiot = parsed[i].longName
                                var sala = parsed[i].nrSali

                                var td = $("<td>" + id + "</td>")
                                tr.append(td)
                                var td = $("<td>" + przedmiot + "</td>")
                                tr.append(td)
                                var td = $("<td>" + sala + "</td>")
                                tr.append(td)

                                $("#Dzisiaj table").append(tr)
                            }
                            DodanieOnClick:
                            {
                                $("#DzisiajLeft *").click(function () {
                                    $("#Dzisiaj").animate({ left: "-100%" }, 'fast')
                                })
                            } 
                        }
                        dodanieTydzien:
                        {
                            var parsed = JSON.parse(response).tydzien
                            console.log(parsed)
                            $("#Tydzien").html("");
                            $("#Tydzien").append("<div id='TydzienLeft'> <img alt='' id='TydzienGradient' src='gfx/gradientBlue1.png'> <img alt='' id='TydzienLeftIcon' src='gfx/top.svg'> <div>");
                            $("#Tydzien").append("<table></table>");
                            $("#Tydzien table").append("<tr><td>Numer lekcji</td><td>PN</td><td>WT</td><td>ŚR</td><td>CZ</td><td>PT</td></tr>")
                            for (var i = 0; i < 14; i++)
                            {
                                var tr = $("<tr></tr>")
                                var nrLekcji = i + 1;
                                var td = $("<td rowspan='2'>" + nrLekcji + "</td>")
                                tr.append(td)
                                for (var j = 0; j < 5; j++)
                                {
                                    var przedmiot = parsed[i+j*14].shortName
                                    var sala = parsed[i+j*14].nrSali
                                    td = $("<td rowspan='2'>" + przedmiot + "<br>" + sala + "</td>")
                                    tr.append(td)
                                }
                                $("#Tydzien table").append(tr)
                                $("#Tydzien table").append("<tr></tr>")
                            }
                        }
                        DodanieOnClick:
                        {
                            $("#TydzienLeft *").click(function () {
                                $("#Tydzien").animate({ left: "-100%" }, 'fast')
                                $("#TydzienLeftIcon").css("z-index", "-6")
                            })
                        }
                    }
                 })
                .fail(function (response) {
                    UI.methods.closeAlertLoading(UI.methods.showAlert(response))
                })
            
        })
        $("#wyborGodzinyReject").click(function ()
        {
            $("#WyborGodziny").animate({ left: "-100%" }, 'fast')
        })
        $("#wyborGodzinyGodziny div").click(function ()
        {
            $("#wyborGodzinyLewySpan").html($(this).html())
        })
        $("#wyborGodzinyMinuty div").click(function ()
        {
            $("#wyborGodzinyPrawySpan").html($(this).html())
        })
        $("#wyborGodzinyAccept").click(function () {
            var wybranaGodzina = $("#wyborGodzinyLewySpan").html()+":"+$("#wyborGodzinyPrawySpan").html() 
            var myTd = $("#Godziny table tr")[myObj.row].children[myObj.column]
            myTd.innerHTML = wybranaGodzina
            myObj.hours = $("#wyborGodzinyLewySpan").html()
            myObj.minutes = $("#wyborGodzinyPrawySpan").html()
            myObj.column = myObj.column;
            Database.methods.updateTables(myObj)
            $("#WyborGodziny").animate({ left: "-100%" }, 'fast')        
        })
 
        DodanieOnclickNaElementachListy();
        DodanieWyjsciaZekranow();
        RozwijanieMenu();
        Captcha.refresh();
	}
}
