var Captcha =
{
    set: 1,
    numberOfSets: 3,
    messages: ["Wybierz obrazki z tęczą", "Wybierz obrazki przedstawiające ciasto", "Wybierz obrazki z jabłkiem"],
    message:"",
    refresh:function()
    {
        this.set = Math.floor(Math.random() * 3) + 1;
        this.message = this.messages[(this.set - 1)];

        if ($("#Captcha"))
            $("#Captcha").remove();
        var captchaObject = $("<div id='Captcha'></div>");

        captchaObject.append("<div id='CaptchaMessage'></div>")
        captchaObject.append("<div id='CaptchaImages'></div>")
        captchaObject.append("<div id='CaptchaConfirm'>Nie jestem robotem</div>")
        captchaObject.append("<div id='CaptchaRefresh'><img id='CaptchaRefreshImg' alt='' src='gfx/Captcha/RefreshImg.png'></div>")

        $("#container").append(captchaObject)
        $("#CaptchaMessage").html("<span>" + this.message + "</span>");

        var imagesToAdd = ["c1.png", "c2.png", "c3.png", "w1.png", "w2.png", "w3.png"];
        /*
        for (var i = 0; i < 5;i++)//;imagesToAdd.length>0;)
        {
            var whichImg = Math.floor(Math.random() * imagesToAdd.length);
            var imgToAdd = imagesToAdd[Math.floor(Math.random() * imagesToAdd.length)];
            $("#CaptchaImages").append("<img alt='' class='CaptchaImageNotSelected' id='CaptchaImages" + i + "'" + " src='gfx/Captcha/" + this.set + "/" + imgToAdd + "'" + ">");
            console.log(imgToAdd)
            console.log("<img alt='' id='CaptchaImages" + whichImg + "'" + " src='gfx/Captcha/" + this.set + "/" + imgToAdd + "'" + ">")  
        }*/
        for (;imagesToAdd.length>0;)
        {
            var whichImg = Math.floor(Math.random() * imagesToAdd.length);
            var imgToAdd = imagesToAdd[whichImg];
            $("#CaptchaImages").append("<img alt='' class='CaptchaImageNotSelected' src='gfx/Captcha/" + this.set + "/" + imgToAdd + "'" + ">");
            imagesToAdd.splice(whichImg, 1);        
        }
        $("#CaptchaImages *").click(function ()
        {
            $(this).toggleClass("CaptchaImageNotSelected");
        })
        $("#CaptchaRefresh").click(function () {
            Captcha.refresh();
        })
        $("#CaptchaConfirm").click(function () {
            function CaptchaOK()
            {
                var allImages = $("#CaptchaImages *");
                var selectedImages = [];
                for (var i = 0; i < 6; i++)
                {
                    if(!allImages[i].className)
                        selectedImages.push(allImages[i]);
                }
                if (selectedImages.length != 3)
                    return false;
                for (var i=0;i<3;i++)
                {
                    if (selectedImages[i].src.charAt(selectedImages[i].src.length - 6) == "w")
                        return false;
                }
                return true;
            }
            if(CaptchaOK())
            {
                Captcha.close();
                UI.methods.showAlert("Prawdopodobnie jesteś człowiekiem - zarejestruj się lub zaloguj");
                RejLog.refresh();
            }
            else
            {
                Captcha.refresh();
            }
        })
        return true
    },
    close:function()
    {
        if ($("#Captcha"))
            $("#Captcha").remove();
    }
}