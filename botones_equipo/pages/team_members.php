<?php
require_once(__DIR__ . '/../../../config.php');
require_login();

global $DB, $OUTPUT, $PAGE;

$context = context_system::instance();
$PAGE->set_context($context);

// Obtener el ID del equipo desde los parámetros de la URL
$teamid = required_param('teamid', PARAM_INT);

// Obtener el nombre del equipo
$team = $DB->get_record('teams', array('id' => $teamid));
if (!$team) {
    throw new moodle_exception('Equipo no encontrado');
}

$PAGE->set_url(new moodle_url('/blocks/botones_equipo/pages/team_members.php', array('teamid' => $teamid)));
$PAGE->set_pagelayout('standard');
$PAGE->set_title('Miembros del Equipo: ' . format_string($team->name));
$PAGE->set_heading('Miembros del Equipo: ' . format_string($team->name));

// Obtener usuarios que pertenecen al equipo
$team_members = $DB->get_records_sql("
    SELECT u.*
    FROM {user} u
    JOIN {team_members} tm ON u.id = tm.userid
    WHERE tm.teamid = :teamid
", array('teamid' => $teamid));

echo $OUTPUT->header();
echo $OUTPUT->heading('Miembros del Equipo: ' . format_string($team->name));

// Mostrar usuarios obtenidos
if ($team_members) {
    echo '<ul>';
    foreach ($team_members as $user) {
        echo '<li>' . fullname($user) . '</li>';
    }
    echo '</ul>';
} else {
    echo '<p>No hay usuarios en este equipo.</p>';
}


echo $OUTPUT->footer();
?>
