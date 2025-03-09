let currentChart = null;

function openChartModal(countType, title) {
    if (currentChart) {
        currentChart.destroy();
    }

    const theme = document.documentElement.getAttribute('data-bs-theme') || 'light';
    const ctx = document.getElementById('chartCanvas').getContext('2d');

    const colors = {
        background: theme === 'dark' ? 'rgba(53, 162, 235, 0.2)' : 'rgba(75, 192, 192, 0.2)',
        border: theme === 'dark' ? 'rgba(53, 162, 235, 1)' : 'rgba(75, 192, 192, 1)'
    };

    const data = {
        UserCount: [12, 19, 3, 5, 2, 3],
        TeamMemberCount: [5, 10, 7, 8, 6, 9],
        ContactCount: [20, 15, 18, 22, 25, 20],
        RoleCount: [3, 4, 5, 6, 7, 8]
    };

    currentChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
            datasets: [{
                label: title,
                data: data[countType],
                backgroundColor: colors.background,
                borderColor: colors.border,
                borderWidth: 2,
                tension: 0.4,
                fill: true
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                title: {
                    display: true,
                    text: title
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    grid: {
                        color: theme === 'dark' ? 'rgba(255, 255, 255, 0.1)' : 'rgba(0, 0, 0, 0.1)'
                    }
                },
                x: {
                    grid: {
                        color: theme === 'dark' ? 'rgba(255, 255, 255, 0.1)' : 'rgba(0, 0, 0, 0.1)'
                    }
                }
            }
        }
    });

    new bootstrap.Modal('#chartModal').show();
}