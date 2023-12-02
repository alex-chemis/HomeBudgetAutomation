CREATE OR REPLACE FUNCTION calculate_percentage_by_item(
    start_date TIMESTAMP,
    end_date TIMESTAMP,
    article_ids INT[],
    flow_type VARCHAR
) RETURNS TABLE (article_1d INT, percentage NUMERIC) AS $$
DECLARE
    total_flow NUMERIC;
BEGIN
    SELECT
        CASE
            WHEN flow_type = 'debit' THEN SUM(debit)
            WHEN flow_type = 'credit' THEN SUM(credit)
            WHEN flow_type = 'amount' THEN SUM(debit - credit)
            ELSE 0
        END
    INTO total_flow
    FROM operations
    WHERE create_date >= start_date
        AND create_date <= end_date;

    FOR article_1d IN SELECT id FROM articles WHERE id = ANY(article_ids) LOOP
        DECLARE
            article_flow NUMERIC;
        BEGIN
            SELECT
                CASE
                    WHEN flow_type = 'debit' THEN SUM(debit)
                    WHEN flow_type = 'credit' THEN SUM(credit)
                    WHEN flow_type = 'amount' THEN SUM(debit - credit)
                    ELSE 0
                END
            INTO article_flow
            FROM operations AS o
            WHERE o.article_id = article_1d
                AND o.create_date >= start_date
                AND o.create_date <= end_date;

            IF total_flow IS NOT NULL AND article_flow IS NOT NULL AND total_flow <> 0 THEN
                percentage := (article_flow / total_flow) * 100;
            ELSE
                percentage := 0;
            END IF;

            RETURN NEXT;
        END;
    END LOOP;
END;
$$ LANGUAGE plpgsql;