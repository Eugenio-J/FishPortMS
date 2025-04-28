function TablePaginate() {
    $(document).ready(function () {

        $('#table').DataTable({
            searching: false,
            paging: false,
            responsive: false,
            info: false,
            ordering: false
        });
    });
}
