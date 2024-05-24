document.addEventListener('DOMContentLoaded', (event) => {
    const sidebar = document.getElementById('sidebar');
    const hideSidebarBtn = document.getElementById('hide-sidebar');
    const showSidebarBtn = document.getElementById('show-sidebar');
    const mainContent = document.getElementById('main-content');
    const projectTabs = document.querySelectorAll('.sidebar-projects-content-item');
    const optionTabs = document.querySelectorAll('.sidebar-options-content-item');

    hideSidebarBtn.addEventListener('click', () => {
        if (window.innerWidth < 768) {
            mainContent.classList.remove('main-hidden');
        }
        sidebar.classList.add('sidebar-hidden');
        mainContent.classList.add('main-expanded');
        showSidebarBtn.classList.remove('sidebar-show-hidden');
    });

    showSidebarBtn.addEventListener('click', () => {
        sidebar.classList.remove('sidebar-hidden');
        if (window.innerWidth < 768) {
            sidebar.classList.add('sidebar-fullscreen');
            mainContent.classList.add('main-hidden');
        }
        mainContent.classList.remove('main-expanded');
        showSidebarBtn.classList.add('sidebar-show-hidden');
    });

    projectTabs.forEach(projectTab => {
        projectTab.addEventListener('click', handleProjectClick);
    });

    optionTabs.forEach(optionTab => {
        optionTab.addEventListener('click', handleOptionClick);
    });

    window.addEventListener('resize', () => {
        adjustSidebar();
    });

    window.addEventListener('load', () => {
        adjustSidebar();
    });

    function adjustSidebar() {
        if (window.innerWidth < 768) {
            sidebar.classList.add('sidebar-hidden');
            mainContent.classList.add('main-expanded');
            showSidebarBtn.classList.remove('sidebar-show-hidden');
        }
    }

    function handleProjectClick(event) {
        optionTabs.forEach(tab => tab.classList.remove('sidebar-options-content-item-active'));
        projectTabs.forEach(tab => tab.classList.remove('sidebar-projects-content-item-active'));
        if (window.innerWidth < 768) {
            sidebar.classList.add('sidebar-hidden');
            mainContent.classList.add('main-expanded');
            showSidebarBtn.classList.remove('sidebar-show-hidden');
            mainContent.classList.remove('main-hidden');
        }
        event.currentTarget.classList.add('sidebar-projects-content-item-active');
    }

    function handleOptionClick(event) {
        projectTabs.forEach(tab => tab.classList.remove('sidebar-projects-content-item-active'));
        optionTabs.forEach(tab => tab.classList.remove('sidebar-options-content-item-active'));
        if (window.innerWidth < 768) {
            sidebar.classList.add('sidebar-hidden');
            mainContent.classList.add('main-expanded');
            showSidebarBtn.classList.remove('sidebar-show-hidden');
            mainContent.classList.remove('main-hidden');
        }
        event.currentTarget.classList.add('sidebar-options-content-item-active');
    }
});

function login() {
    var token = "received_jwt_token";
    localStorage.setItem('jwtToken', token);
}

function getToken() {
    return localStorage.getItem('jwtToken');
}