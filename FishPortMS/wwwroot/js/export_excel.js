function export_excel(data) {
    $(document).ready(function () {
        $('#export-excel').click(function () {
            var table2excel = new Table2Excel();
            table2excel.export($('table'));
        });
    });
}
