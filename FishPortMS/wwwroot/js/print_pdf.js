function print_pdf(filename) {
    $(document).ready(function () {
        $("#print_pdf").on("click", function () {
            $("#mainContent").hide();
            $("#printableContent").show();
            var invoice = $("#printableContent");

            var opt = {
                filename: filename || 'default_invoice.pdf', // fallback if no filename is passed
                image: { type: 'jpeg', quality: 0.98 },
                html2canvas: { scale: 2 },
                jsPDF: { unit: 'in', format: 'legal', orientation: 'portrait' }
            };

            html2pdf().from(invoice[0]).set(opt).save().then(function () {
                $("#mainContent").show();
            });
        });
    });
}
