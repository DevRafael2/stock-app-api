CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    password VARCHAR(355) NOT NULL
);

CREATE TABLE products (
	id SERIAL PRIMARY KEY,
	name VARCHAR(100) NOT NULL,
	amount INT NOT NULL DEFAULT 0
);

CREATE TABLE stock_history_products (
	id SERIAL PRIMARY KEY,
	product_id INTEGER NOT NULL,
	user_id UUID NOT NULL,
	amount INTEGER NOT NULL DEFAULT 0,
	created_at DATE DEFAULT CURRENT_DATE,

	CONSTRAINT fk_product
        FOREIGN KEY(product_id)
        REFERENCES products(id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,

	CONSTRAINT fk_user
        FOREIGN KEY(user_id)
        REFERENCES users(id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);

INSERT INTO users (name, email, password) VALUES ('UserAdmin', 'test@gmail.com', '15e2b0d3c33891ebb0f1ef609ec419420c20e320ce94c65fbc8c3312448eb225');
INSERT INTO products (name, amount) VALUES ('Teclado', 10), ('Mouse', 15), ('Monitor 12 pulgadas', 18) 



