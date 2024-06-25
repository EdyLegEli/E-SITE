<?php
require_once(__DIR__ . '/../../../config.php');
require_login();

// Incluir las clases necesarias de Moodle Forms
require_once($CFG->libdir.'/formslib.php');

class add_team_form extends moodleform {

    public function definition() {
        $mform = $this->_form;

        // Elemento para el nombre del equipo
        $mform->addElement('text', 'teamname', 'Nombre del Equipo');
        $mform->setType('teamname', PARAM_TEXT);
        $mform->addRule('teamname', 'Este campo es obligatorio', 'required', null, 'client');

        // Elemento para la descripción del equipo
        $mform->addElement('textarea', 'description', 'Descripción del Equipo');
        $mform->setType('description', PARAM_TEXT);
        $mform->addRule('description', 'Este campo es obligatorio', 'required', null, 'client');

        // Botón de envío
        $this->add_action_buttons(true, 'Añadir Equipo');
    }

    public function validation($data, $files) {
        $errors = parent::validation($data, $files);

        // Validar que los campos no estén vacíos
        if (empty($data['teamname'])) {
            $errors['teamname'] = 'Por favor, introduce el nombre del equipo.';
        }
        if (empty($data['description'])) {
            $errors['description'] = 'Por favor, introduce la descripción del equipo.';
        }

        return $errors;
    }
}

// Procesamiento del formulario
$form = new add_team_form();

if ($form->is_cancelled()) {
    // Manejar cancelación del formulario si se implementa un botón "Cancelar"
    redirect(new moodle_url('/blocks/botones_equipo/pages/equipos.php'));
} elseif ($formdata = $form->get_data()) {
    // Procesar datos cuando el formulario se envía correctamente
    $newteam = new stdClass();
    $newteam->name = $formdata->teamname;
    $newteam->description = $formdata->description;
    $newteam->timecreated = time();

    $DB->insert_record('teams', $newteam);

    redirect(new moodle_url('/blocks/botones_equipo/pages/equipos.php'), 'Equipo añadido con éxito');
}

// Mostrar la página con el formulario
echo $OUTPUT->header();
echo $OUTPUT->heading('Añadir Equipo Nuevo');
$form->display();
echo $OUTPUT->footer();
?>

