CREATE TABLE users (
    id UUID PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(200) NOT NULL UNIQUE,
    password_hash TEXT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE TABLE vehicles (
    id UUID PRIMARY KEY,
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    name VARCHAR(100) NOT NULL,
    brand VARCHAR(100) NOT NULL,
    model VARCHAR(100) NOT NULL,
    registration_number VARCHAR(50) NOT NULL,
    vehicle_type VARCHAR(30) NOT NULL,
    purchase_date DATE NOT NULL,
    current_mileage INT NOT NULL,
    UNIQUE(user_id, registration_number)
);

CREATE TABLE mileage_logs (
    id UUID PRIMARY KEY,
    vehicle_id UUID NOT NULL REFERENCES vehicles(id) ON DELETE CASCADE,
    mileage INT NOT NULL,
    date_recorded TIMESTAMP NOT NULL
);
CREATE INDEX idx_mileage_vehicle_date ON mileage_logs(vehicle_id, date_recorded);

CREATE TABLE fuel_logs (
    id UUID PRIMARY KEY,
    vehicle_id UUID NOT NULL REFERENCES vehicles(id) ON DELETE CASCADE,
    fuel_amount_litres NUMERIC(10,2) NOT NULL,
    fuel_cost NUMERIC(10,2) NOT NULL,
    mileage_at_fill INT NOT NULL,
    date TIMESTAMP NOT NULL
);
CREATE INDEX idx_fuel_vehicle_date ON fuel_logs(vehicle_id, date);

CREATE TABLE service_schedules (
    id UUID PRIMARY KEY,
    vehicle_id UUID NOT NULL REFERENCES vehicles(id) ON DELETE CASCADE,
    service_name VARCHAR(100) NOT NULL,
    last_service_date DATE NOT NULL,
    last_service_mileage INT NOT NULL,
    service_interval_months INT NOT NULL,
    service_interval_km INT NOT NULL
);

CREATE TABLE vehicle_documents (
    id UUID PRIMARY KEY,
    vehicle_id UUID NOT NULL REFERENCES vehicles(id) ON DELETE CASCADE,
    document_type INT NOT NULL,
    file_path TEXT NOT NULL,
    issue_date DATE NOT NULL,
    expiry_date DATE NOT NULL
);
CREATE INDEX idx_doc_expiry ON vehicle_documents(vehicle_id, expiry_date);

CREATE TABLE part_replacements (
    id UUID PRIMARY KEY,
    vehicle_id UUID NOT NULL REFERENCES vehicles(id) ON DELETE CASCADE,
    part_name VARCHAR(100) NOT NULL,
    replacement_date DATE NOT NULL,
    mileage_at_replacement INT NOT NULL,
    notes TEXT
);
CREATE INDEX idx_part_date ON part_replacements(vehicle_id, replacement_date);
