<?php
defined('MOODLE_INTERNAL') || die();

if ($ADMIN->fulltree) {
    $settings->add(new admin_setting_heading('block_botones_home_settings', '', get_string('pluginname', 'block_botones_home')));
    // Puedes agregar configuraciones adicionales aquí si es necesario
}
?>
