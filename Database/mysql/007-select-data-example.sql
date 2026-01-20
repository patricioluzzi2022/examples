use BaseDeEjemplo;

select e.nombre, m.materia from estudiante_materia as em
left join estudiantes e on em.estudiante = e.id
left join materias m on em.materia = m.id