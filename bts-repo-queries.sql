select * from t_m_package

select * from t_r_exam
update t_r_exam set acceptance_status_id = null where id = 1

select * from t_r_exam_package
update t_r_exam_package set is_submitted = null, exam_start_datetime = null, reviewer_notes = null, reviewer_score = null where id = 1

select * from t_r_candidate_answer
TRUNCATE TABLE t_r_candidate_answer;

select * from t_m_user

select * from t_m_acceptance_status

-- get all exams
SELECT 
    e.id,
    can.full_name,
    p.package_name,
    e.created_at,
    ep.is_submitted,
    acs.status_name 
from 
    t_r_exam e 
join 
    t_m_user can ON e.candidate_id = can.id 
LEFT JOIN 
    t_m_acceptance_status acs ON e.acceptance_status_id = acs.id 
join  
    t_r_exam_package ep ON e.id = ep.exam_id 
join 
    t_m_package p ON ep.package_id = p.id;

-- get exam by id
SELECT
    e.id, 
    can.full_name AS candidate_name, 
    rev.full_name AS reviewer_name, 
    p.package_name, 
    e.created_at, 
    ep.is_submitted, 
    ep.reviewer_score, 
    ep.reviewer_notes, 
	q.question, 
	qi.file_content AS question_image,
	qi.file_extension AS question_image_extension,
	ca.answer_content, 
	mco.option_char, 
	oi.file_content AS option_image,
	oi.file_extension AS option_image_extension, 
	mco.option_text, 
    acs.status_name 
FROM 
    t_r_exam e 
LEFT JOIN 
    t_m_acceptance_status acs ON e.acceptance_status_id = acs.id 
JOIN 
    t_m_user can ON e.candidate_id = can.id 
JOIN 
    t_m_user rev ON e.reviewer_id = rev.id 
JOIN 
    t_r_exam_package ep ON e.id = ep.exam_id 
JOIN 
    t_m_package p ON ep.package_id = p.id 
LEFT JOIN 
    t_r_candidate_answer ca ON ep.id = ca.exam_package_id 
LEFT JOIN 
    t_m_question q ON ca.question_id = q.id 
LEFT JOIN 
    t_m_multiple_choice_option mco ON ca.choice_option_id = mco.id 
LEFT JOIN 
    t_m_file qi ON q.image_id = qi.id 
LEFT JOIN 
    t_m_file oi ON mco.option_image_id = oi.id
WHERE e.id = 1

-- get all packages by reviewer
SELECT 
    p.id,
    p.package_name, 
    p.package_code 
FROM 
    t_m_package p
JOIN 
    t_r_exam_package ep ON p.id = ep.package_id
JOIN 
	t_r_exam e ON ep.exam_id = e.id
JOIN 
	t_m_user u ON e.reviewer_id = u.id
WHERE 
	e.reviewer_id = 7
GROUP BY
	p.id,
	p.package_name, 
    p.package_code

-- get all question by package
SELECT 
	q.id, 
	q.question,
	q_f.file_content AS question_image,
	q_f.file_extension AS question_image_extension,
	mco.option_char,
	mco.option_text,
	mco.is_correct,
	o_f.file_content AS option_image,
	o_f.file_extension AS option_image_extension
FROM 
	t_m_question q 
LEFT JOIN 
	t_m_file q_f ON q.image_id = q_f.id
LEFT JOIN 
	t_m_multiple_choice_option mco ON q.id = mco.question_id 
LEFT JOIN 
	t_m_file o_f ON mco.option_image_id = o_f.id
WHERE
	q.package_id = 1

-- get all candidate document by candidate_id
SELECT 
    cd.id
	f.file_content,
	f.file_extension,
	dt.type_name
FROM
	t_r_candidate_document cd
JOIN
	t_m_user u ON cd.candidate_id = u.id
JOIN
	t_m_document_type dt ON cd.document_type_id = dt.id
JOIN
	t_m_file f ON cd.file_id = f.id
WHERE
	cd.candidate_id = 4

-- get exam by candidate
SELECT
    e.id, 
    can.full_name AS candidate_name, 
    p.package_name, 
    ep.is_submitted,
	ep.duration, 
	q.question, 
	q.id AS question_id,
	qi.file_content AS question_image,
	qi.file_extension AS question_image_extension,
	mco.option_char, 
	oi.file_content AS option_image,
	oi.file_extension AS option_image_extension, 
	mco.option_text
FROM 
    t_r_exam e 
JOIN 
    t_m_user can ON e.candidate_id = can.id 
JOIN 
    t_r_exam_package ep ON e.id = ep.exam_id 
JOIN 
    t_m_package p ON ep.package_id = p.id 
JOIN 
    t_m_question q ON p.id = q.package_id 
LEFT JOIN 
    t_m_multiple_choice_option mco ON q.id = mco.question_id
LEFT JOIN 
    t_m_file qi ON q.image_id = qi.id 
LEFT JOIN 
    t_m_file oi ON mco.option_image_id = oi.id
WHERE 
	can.id = 1

SELECT 
    e.id,
	can.id AS candidate_id,
    can.full_name,
	p.id AS package_id,
    p.package_name,
    e.created_at,
	ep.id AS exam_package_id,
    ep.is_submitted,
	acs.id AS acceptance_status_id,
    acs.status_name 
FROM 
    t_r_exam e 
JOIN 
    t_m_user can ON e.candidate_id = can.id 
LEFT JOIN 
    t_m_acceptance_status acs ON e.acceptance_status_id = acs.id 
JOIN 
    t_r_exam_package ep ON e.id = ep.exam_id 
JOIN 
    t_m_package p ON ep.package_id = p.id

-- get exam list by reviewer
SELECT 
    e.id,
    can.full_name,
    p.package_name,
    e.created_at,
    ep.is_submitted,
    acs.status_name 
FROM 
    t_r_exam e 
JOIN 
    t_m_user can ON e.candidate_id = can.id 
LEFT JOIN 
    t_m_acceptance_status acs ON e.acceptance_status_id = acs.id 
JOIN 
    t_r_exam_package ep ON e.id = ep.exam_id 
JOIN 
    t_m_package p ON ep.package_id = p.id 
WHERE 
    e.reviewer_id = @reviewer_i
    