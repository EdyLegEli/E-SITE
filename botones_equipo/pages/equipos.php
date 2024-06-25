<?php
require_once(__DIR__ . '/../../../config.php');
require_login();

global $DB, $OUTPUT, $PAGE;

$context = context_system::instance();
$PAGE->set_context($context);
$PAGE->set_url(new moodle_url('/blocks/botones_equipo/pages/equipos.php'));
$PAGE->set_pagelayout('standard');
$PAGE->set_title('Equipos');
$PAGE->set_heading('Equipos');

// Obtener datos de la tabla de equipos.
$teams = $DB->get_records('teams', array(), 'timecreated DESC');

echo $OUTPUT->header();
echo $OUTPUT->heading('Equipos');

// Mostrar datos obtenidos.
if ($teams) {
    echo '<ul>';
    foreach ($teams as $team) {
        $url_team_members = new moodle_url('/blocks/botones_equipo/pages/team_members.php', array('teamid' => $team->id));

        echo '<li>';
        echo html_writer::link($url_team_members, format_string($team->name));
        echo '</li>';
    }
    echo '</ul>';
} else {
    echo '<p>No se encontraron equipos.</p>';
}

// Añadir botón para ir a la página de añadir equipo.
$url_add_team = new moodle_url('/blocks/botones_equipo/pages/add_team.php');
echo $OUTPUT->single_button($url_add_team, 'Añadir Equipo Nuevo');

echo $OUTPUT->footer();
?>
