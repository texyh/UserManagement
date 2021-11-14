CREATE TABLE IF NOT EXISTS check_point (
    id serial primary key,
    last_check_point int,
    data text
)