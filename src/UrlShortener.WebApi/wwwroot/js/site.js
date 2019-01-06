(function initMainPage() {
    $(document).on('show.bs.tooltip', function (e) {
        setTimeout(function () {   //calls click event after a certain time
            $('[data-toggle="tooltip"]').tooltip('hide');
        }, 200);
    });

    $(document).on('click', '.copyShortUrl', function (e) {
        var copyText = $(this).siblings(".short-url-link").attr("href");
        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val(copyText).select();
        document.execCommand("copy");
        $temp.remove();

        $(this).tooltip('show');
    });

    $(document).ready(function () {
        $("#shortenUrlForm").on("submit", function (evt) {
            $("#msg").html("");
            $(this).removeClass('was-validated');

            if (!this.checkValidity()) {
                evt.preventDefault();
                evt.stopPropagation();
                return;
            }

            evt.preventDefault();
            $(this).addClass('was-validated');

            var obj = {};
            obj.url = $("#shorten-url").val();

            $.ajax({
                url: "/api/shortenUrl",
                type: "POST",
                data: JSON.stringify(obj),
                contentType: "application/json",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("MY-XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                }
            })
                .done(function (data) {
                    var url = document.location.origin + "/" + data.shortUrl;
                    $("#result").prepend(`
                            <div class="card">
                                <div class="card-header">
                                    <span><strong>` + data.longUrl + `</strong></span>
                                </div>
                                <div class="card-body">
                                    <a class="short-url-link" href=` + url + `>` + url + `</a>
                                    <button class="btn btn-outline-success btn-sm copyShortUrl" data-toggle="tooltip" data-trigger="click" data-animation="true" title="Copied!">Copy</button>
                                    <a class="float-right text-secondary" href=` + url + `/stats>stats</a>
                                </div>
                            </div>`);

                    $("#shorten-url").removeClass('is-invalid');
                })
                .fail(function (xhr) {
                    $("#shorten-url").addClass('is-invalid');
                    $("#msg").html("Please enter a valid url.");
                });
        });

    });
}());