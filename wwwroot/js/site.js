document.addEventListener('DOMContentLoaded', function () {
    var ctx = document.getElementById('cryptoChart').getContext('2d');
    var prices = @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.Select(p => p.Price)));
    var times = @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.Select(p => p.Time.ToString("yyyy-MM-dd HH:mm:ss"))));

    new Chart(ctx, {
        type: 'line',
        data: {
            labels: times,
            datasets: [{
                label: 'Bitcoin Price (USD)',
                data: prices,
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                x: {
                    type: 'time',
                    time: {
                        unit: 'minute'
                    }
                },
                y: {
                    beginAtZero: false
                }
            }
        }
    });
});
