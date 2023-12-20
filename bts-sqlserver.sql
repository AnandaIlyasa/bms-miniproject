CREATE TABLE t_m_file (
	id INT IDENTITY,
	file_content TEXT NOT NULL,
	file_extension VARCHAR(5) NOT NULL,

	created_by INT NOT NULL,
	created_at DATETIME NOT NULL,
	updated_at DATETIME,
	updated_by INT,
	ver ROWVERSION NOT NULL,
	is_active BIT NOT NULL,

	CONSTRAINT file_pk PRIMARY KEY(id)
);

CREATE TABLE t_m_package (
	id INT IDENTITY,
    package_code VARCHAR(10) NOT NULL,
    package_name VARCHAR(30) NOT NULL,

	created_by INT NOT NULL,
	created_at DATETIME NOT NULL,
	updated_at DATETIME,
	updated_by INT,
	ver ROWVERSION NOT NULL,
	is_active BIT NOT NULL,

	CONSTRAINT package_pk PRIMARY KEY(id),
    CONSTRAINT package_bk UNIQUE(package_code)
);

CREATE TABLE t_m_question (
	id INT IDENTITY,
    question TEXT,
    package_id INT NOT NULL,
	image_id INT,

	created_by INT NOT NULL,
	created_at DATETIME NOT NULL,
	updated_at DATETIME,
	updated_by INT,
	ver ROWVERSION NOT NULL,
	is_active BIT NOT NULL,

	CONSTRAINT question_pk PRIMARY KEY(id),
    CONSTRAINT question_package_fk FOREIGN KEY(package_id) REFERENCES t_m_package(id),
    CONSTRAINT question_image_fk FOREIGN KEY(image_id) REFERENCES t_m_file(id)
);

CREATE TABLE t_m_multiple_choice_option (
	id INT IDENTITY,
    option_char CHAR(1) NOT NULL,
    option_text VARCHAR(255),
    is_correct BIT NOT NULL,
    question_id INT NOT NULL,
    option_image_id INT,

	created_by INT NOT NULL,
	created_at DATETIME NOT NULL,
	updated_at DATETIME,
	updated_by INT,
	ver ROWVERSION NOT NULL,
	is_active BIT NOT NULL,

	CONSTRAINT multiple_choice_option_pk PRIMARY KEY(id),
    CONSTRAINT multiple_choice_option_image_fk FOREIGN KEY(option_image_id) REFERENCES t_m_file(id),
    CONSTRAINT multiple_choice_option_question_fk FOREIGN KEY(question_id) REFERENCES t_m_question(id)
);

CREATE TABLE t_m_role (
	id INT IDENTITY,
    role_code VARCHAR(10) NOT NULL,
	role_name VARCHAR(20) NOT NULL,

	created_by INT NOT NULL,
	created_at DATETIME NOT NULL,
	updated_at DATETIME,
	updated_by INT,
	ver ROWVERSION NOT NULL,
	is_active BIT NOT NULL,

	CONSTRAINT role_pk PRIMARY KEY(id),
	CONSTRAINT role_bk UNIQUE(role_code)
);

CREATE TABLE t_m_user (
	id INT IDENTITY,
    full_name VARCHAR(30) NOT NULL,
	email VARCHAR(30) NOT NULL,
	pass TEXT NOT NULL,
	photo_id INT,
	role_id INT NOT NULL,

	created_by INT NOT NULL,
	created_at DATETIME NOT NULL,
	updated_at DATETIME,
	updated_by INT,
	ver ROWVERSION NOT NULL,
	is_active BIT NOT NULL,

	CONSTRAINT user_pk PRIMARY KEY(id),
	CONSTRAINT user_bk UNIQUE(email),
	CONSTRAINT user_photo_fk FOREIGN KEY(photo_id) REFERENCES t_m_file(id),
	CONSTRAINT user_role_fk FOREIGN KEY(role_id) REFERENCES t_m_role(id)
);

CREATE TABLE t_m_document_type (
	id INT IDENTITY,
	code VARCHAR(5) NOT NULL,
	type_name VARCHAR(30) NOT NULL,

	created_by INT NOT NULL,
	created_at DATETIME NOT NULL,
	updated_at DATETIME,
	updated_by INT,
	ver ROWVERSION NOT NULL,
	is_active BIT NOT NULL,

	CONSTRAINT document_type_pk PRIMARY KEY(id),
	CONSTRAINT document_type_bk UNIQUE(code)
);

CREATE TABLE t_r_candidate_document (
    id INT IDENTITY,
    candidate_id INT NOT NULL,
	file_id INT NOT NULL,
	document_type_id INT NOT NULL,

	created_by INT NOT NULL,
	created_at DATETIME NOT NULL,
	updated_at DATETIME,
	updated_by INT,
	ver ROWVERSION NOT NULL,
	is_active BIT NOT NULL,

    CONSTRAINT candidate_document_pk PRIMARY KEY(id),
	CONSTRAINT candidate_document_ck UNIQUE(candidate_id, document_type_id, file_id),
    CONSTRAINT candidate_document_user_fk FOREIGN KEY(candidate_id) REFERENCES t_m_user(id),
	CONSTRAINT candidate_file_fk FOREIGN KEY(file_id) REFERENCES t_m_file(id),
    CONSTRAINT candidate_document_type_fk FOREIGN KEY(document_type_id) REFERENCES t_m_document_type(id)
);

CREATE TABLE t_m_acceptance_status (
    id INT IDENTITY,
	code VARCHAR(5) NOT NULL,
    status_name VARCHAR(20) NOT NULL,

	created_by INT NOT NULL,
	created_at DATETIME NOT NULL,
	updated_at DATETIME,
	updated_by INT,
	ver ROWVERSION NOT NULL,
	is_active BIT NOT NULL,

    CONSTRAINT acceptance_status_pk PRIMARY KEY(id),
	CONSTRAINT acceptance_status_bk UNIQUE(code)
);

CREATE TABLE t_r_exam (
    id INT IDENTITY,
    candidate_id INT NOT NULL,
    reviewer_id INT NOT NULL,
	login_start DATETIME NOT NULL,
	login_end DATETIME NOT NULL,
    acceptance_status_id INT,

	created_by INT NOT NULL,
	created_at DATETIME NOT NULL,
	updated_at DATETIME,
	updated_by INT,
	ver ROWVERSION NOT NULL,
	is_active BIT NOT NULL,
    
    CONSTRAINT exam_pk PRIMARY KEY(id),

    CONSTRAINT exam_candidate_fk FOREIGN KEY(candidate_id) REFERENCES t_m_user(id),
    CONSTRAINT exam_reviewer_fk FOREIGN KEY(reviewer_id) REFERENCES t_m_user(id),
	CONSTRAINT exam_result_acceptance_status_fk FOREIGN KEY(acceptance_status_id) REFERENCES t_m_acceptance_status(id)
);

CREATE TABLE t_r_exam_package (
	id INT IDENTITY,
	package_id INT NOT NULL,
	exam_id INT NOT NULL,
	exam_start_datetime DATETIME,
    duration INT NOT NULL,
	is_submitted BIT,
	reviewer_notes TEXT,
    reviewer_score FLOAT,

	created_by INT NOT NULL,
	created_at DATETIME NOT NULL,
	updated_at DATETIME,
	updated_by INT,
	ver ROWVERSION NOT NULL,
	is_active BIT NOT NULL,

	CONSTRAINT exam_package_pk PRIMARY KEY(id),
	CONSTRAINT exam_package_id_fk FOREIGN KEY(package_id) REFERENCES t_m_package(id),
	CONSTRAINT exam_id_fk FOREIGN KEY(exam_id) REFERENCES t_r_exam(id)
);

CREATE TABLE t_r_candidate_answer (
    id INT IDENTITY,
	answer_content TEXT,
    exam_package_id INT NOT NULL,
    choice_option_id INT,
	question_id INT NOT NULL,

	created_by INT NOT NULL,
	created_at DATETIME NOT NULL,
	updated_at DATETIME,
	updated_by INT,
	ver ROWVERSION NOT NULL,
	is_active BIT NOT NULL,

    CONSTRAINT candidate_answer_pk PRIMARY KEY(id),
	CONSTRAINT candidate_answer_ck UNIQUE(exam_package_id, choice_option_id, question_id),
    CONSTRAINT candidate_exam_fk FOREIGN KEY(exam_package_id) REFERENCES t_r_exam_package(id),
    CONSTRAINT candidate_multiple_choice_answer_fk FOREIGN KEY(choice_option_id) REFERENCES t_m_multiple_choice_option(id),
    CONSTRAINT candidate_question_fk FOREIGN KEY(question_id) REFERENCES t_m_question(id)
);

-- INSERT INTO t_m_file (file_content, file_extension, created_by, created_at, is_active) VALUES
-- 	('file_1', 'jpg', 1, GETDATE(), 1),
-- 	('file_2', 'png', 1, GETDATE(), 1),
-- 	('file_3', 'jpeg', 1, GETDATE(), 1),
-- 	('file_4', 'jpg', 1, GETDATE(), 1),
-- 	('file_5', 'jpg', 1, GETDATE(), 1);

-- INSERT INTO t_m_package (package_code, package_name, created_by, created_at, is_active) VALUES
-- 	('JAVA-1', 'Bootcamp JAVA batch-1', 1, GETDATE(), 1),
-- 	('JAVA-2', 'Bootcamp JAVA batch-2', 1, GETDATE(), 1),
-- 	('JAVA-3', 'Bootcamp JAVA batch-3', 1, GETDATE(), 1),
-- 	('JAVA-4', 'Bootcamp JAVA batch-4', 1, GETDATE(), 1),
-- 	('JAVA-5', 'Bootcamp JAVA batch-5', 1, GETDATE(), 1);

-- INSERT INTO t_m_question (question, package_id, image_id, created_by, created_at, is_active) VALUES
-- 	('Sebutkan prinsip SOLID ke-1', 1, NULL, 1, GETDATE(), 1),
-- 	('Sebutkan prinsip SOLID ke-2', 1, NULL, 1, GETDATE(), 1),
-- 	('Sebutkan prinsip SOLID ke-3', 3, NULL, 1, GETDATE(), 1),
-- 	(NULL, 1, 4, 1, GETDATE(), 1),
-- 	(NULL, 5, 5, 1, GETDATE(), 1);

-- INSERT INTO t_m_multiple_choice_option (option_char, option_text, is_correct, question_id, option_image_id, created_by, created_at, is_active) VALUES
-- 	('A', 'Single responsibility', 1, 1, NULL, 1, GETDATE(), 1),
-- 	('B', 'Open-closed', 1, 2, NULL, 1, GETDATE(), 1),
-- 	('C', 'Liskov substitution', 1, 3, NULL, 1, GETDATE(), 1),
-- 	('D', NULL, 0, 4, 1, 1, GETDATE(), 1),
-- 	('E', NULL, 0, 5, 2, 1, GETDATE(), 1);

INSERT INTO t_m_role (role_code, role_name, created_by, created_at, is_active) VALUES
	('SA', 'Super Admin', 1, GETDATE(), 1),
	('HR', 'HR', 1, GETDATE(), 1),
	('RVW', 'Reviewer', 1, GETDATE(), 1),
	('CNDT', 'Candidate', 1, GETDATE(), 1);

INSERT INTO t_m_user (full_name, email, pass, photo_id, role_id, created_by, created_at, is_active) VALUES
	('Andi Susilo', 'andi', 'andi', NULL, 1, 1, GETDATE(), 1);
	-- ('Budi Doremi', 'budi', 'budi', NULL, 2, 1, GETDATE(), 1),
	-- ('Caca Putri', 'caca', 'caca', NULL, 3, 1, GETDATE(), 1),
	-- ('Deni Putra', 'deni', 'deni', NULL, 4, 1, GETDATE(), 1),
	-- ('Eka Setiawan', 'eka', 'eka', NULL, 4, 1, GETDATE(), 1);

INSERT INTO t_m_document_type (code, type_name, created_by, created_at, is_active) VALUES
	('CV', 'CV', 1, GETDATE(), 1),
	('CERT', 'Ijazah', 1, GETDATE(), 1),
	('TRAN', 'Transkrip Nilai', 1, GETDATE(), 1),
	('KK', 'KK', 1, GETDATE(), 1);

-- INSERT INTO t_r_candidate_document (candidate_id, file_id, document_type_id, created_by, created_at, is_active) VALUES
-- 	(1, 1, 1, 1, GETDATE(), 1),
-- 	(1, 2, 2, 1, GETDATE(), 1),
-- 	(1, 3, 3, 1, GETDATE(), 1),
-- 	(1, 4, 4, 1, GETDATE(), 1),
-- 	(2, 5, 4, 1, GETDATE(), 1);

INSERT INTO t_m_acceptance_status (code, status_name, created_by, created_at, is_active) VALUES
	('NR', 'needs review', 1, GETDATE(), 1),
	('REJ', 'rejected', 1, GETDATE(), 1),
	('CON', 'considered', 1, GETDATE(), 1),
	('REC', 'recommended', 1, GETDATE(), 1);

-- INSERT INTO t_r_exam (candidate_id, reviewer_id, login_start, login_end, acceptance_status_id, created_by, created_at, is_active) VALUES
-- 	(1, 3, GETDATE(), GETDATE(), NULL, 1, GETDATE(), 1),
-- 	(2, 3, GETDATE(), GETDATE(), 1, 1, GETDATE(), 1),
-- 	(3, 3, GETDATE(), GETDATE(), 2, 1, GETDATE(), 1),
-- 	(4, 3, GETDATE(), GETDATE(), 3, 1, GETDATE(), 1),
-- 	(5, 3, GETDATE(), GETDATE(), 4, 1, GETDATE(), 1);

-- INSERT INTO t_r_exam_package (package_id, exam_id, exam_start_datetime, duration, is_submitted, reviewer_notes, reviewer_score, created_by, created_at, is_active) VALUES
-- 	(1, 1, NULL, 20, NULL, NULL, NULL, 1, GETDATE(), 1),
-- 	(2, 2, NULL, 20, NULL, NULL, NULL, 1, GETDATE(), 1),
-- 	(3, 3, NULL, 20, NULL, NULL, NULL, 1, GETDATE(), 1),
-- 	(4, 4, GETDATE(), 20, 0, 'Belajar lagi yaa', 95, 1, GETDATE(), 1),
-- 	(5, 5, GETDATE(), 20, 0, 'Belajar lagi yaa', 95, 1, GETDATE(), 1);

-- INSERT INTO t_r_candidate_answer (exam_package_id, choice_option_id, answer_content, question_id, created_by, created_at, is_active) VALUES
-- 	(1, NULL, 'HTML adalah programming language', 1, 1, GETDATE(), 1),
-- 	(2, NULL, 'HTML adalah programming language', 2, 1, GETDATE(), 1),
-- 	(3, NULL, 'HTML adalah programming language', 3, 1, GETDATE(), 1),
-- 	(1, 1, NULL, 4, 1, GETDATE(), 1),
-- 	(1, 2, NULL, 5, 1, GETDATE(), 1);
