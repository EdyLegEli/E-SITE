<?php
// This file is part of Moodle - http://moodle.org/
//
// Moodle is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Moodle is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Moodle.  If not, see <http://www.gnu.org/licenses/>.

/**
 * Form for editing HTML block instances.
 *
 * @package   block_botones_home
 * @license   http://www.gnu.org/copyleft/gpl.html GNU GPL v3 or later
 */
class block_botones_home extends block_base {
    
    function init() {
        $this->title = get_string('name', 'block_botones_home');
    }

    function has_config() {
        return true;
    }

    function get_content() {
        global $OUTPUT;

        if ($this->content !== NULL) {
            return $this->content;
        }

        $this->content = new stdClass();
        $content = '';

        // Botón 1: Total de Cursos
        $url_courses = new moodle_url('/my/courses.php');
        $button_courses = $OUTPUT->single_button($url_courses, get_string('totalcursos', 'block_botones_home'));

        // Botón 2: Total de Usuarios
        $url_users = new moodle_url('/admin/user.php');
        $button_users = $OUTPUT->single_button($url_users, get_string('totalusuarios', 'block_botones_home'));

        $content .= $button_courses;
        $content .= $button_users;

        // Configuración de contenido y pie de página
        $this->content->text = $content;
        $this->content->footer = get_string('accesorapido', 'block_botones_home');

        return $this->content;
    }
}
?>
