// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function myFunction() {
    var input, filter, table, tr, td, i, j, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");

    // Loopar genom alla rader i tabellen
    for (i = 1; i < tr.length; i++) { 
        var rowMatch = false;
        td = tr[i].getElementsByTagName("td");

        // Loopar genom alla kolumner i raden
        for (j = 0; j < td.length; j++) {
            if (td[j]) {
                txtValue = td[j].textContent || td[j].innerText;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    rowMatch = true;
                    break; // Stoppa loopen om vi hittar en matchning
                }
            }
        }

        // Visa raden om det fanns en matchning, annars dölj den
        tr[i].style.display = rowMatch ? "" : "none";
    }
}

