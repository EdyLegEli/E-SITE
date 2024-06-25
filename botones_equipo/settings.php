<?php
defined('MOODLE_INTERNAL') || die();

if ($ADMIN->fulltree) {
    $settings->add(new admin_setting_heading('block_botones_equipo_settings', '', get_string('pluginname', 'block_botones_equipo')));
    // Puedes agregar configuraciones adicionales aquí si es necesario
}
?>
