let calendar = null;

async function initCalendar() {
        
    const calendarEl = document.getElementById('calendar');
    calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'multiMonthFourMonth',
        views: {
            multiMonthFourMonth: {
                type: 'multiMonth',
                duration: { months: 4 }
            }
        },
        locale: 'es',
        firstDay: 1,
        height: '40vw',
        aspectRatio: 1.5,
        themeSystem : 'bootstrap5',
        dayCellDidMount: function(info) {
            const date = info.date;
            const ymd = formatYmd(date);
            const today = new Date();
            today.setHours(0, 0, 0, 0);
            const isPast = date < today;

            if (isClanBattleDay(date)) {
                if (isPast) {
                    if (currentMatchDates.has(ymd)) {
                        info.el.classList.add('fc-day-clan-battle-past-yes');
                    } else {
                        info.el.classList.add('fc-day-clan-battle-past-no');
                    }
                } else {
                    info.el.classList.add('fc-day-clan-battle-future');
                }
            }
        },
        dateClick: function(info) {
            loadMatchesForDate(info.dateStr);
        }
    });
    calendar.render();
}