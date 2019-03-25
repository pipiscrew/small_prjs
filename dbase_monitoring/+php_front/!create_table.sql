﻿#
# Structure for table "aaa"
#

CREATE TABLE `aaa` (
  `aaa_id` int(11) NOT NULL AUTO_INCREMENT,
  `bbb_file` tinyint(11) NOT NULL,
  `ccc_file` tinyint(11) NOT NULL,
  `ddd_file` tinyint(11) NOT NULL,
  `honey_file_count` tinyint(4) NOT NULL,
  `honey_sums` text COLLATE utf8_unicode_ci NOT NULL,
  `db_honey_bbb_sum` double NOT NULL,
  `db_honey_ccc_ddd_sum` double NOT NULL,
  `bbb_file_sum` double NOT NULL,
  `bbb_db_sum` double NOT NULL,
  `ccc_file_sum` double NOT NULL,
  `ccc_db_sum` double NOT NULL,
  `ddd_file_sum` double NOT NULL,
  `ddd_db_sum` double NOT NULL,
  `customer_file` tinyint(4) NOT NULL,
  `product_file` tinyint(4) NOT NULL,
  `invoice_file` tinyint(4) NOT NULL,
  `first_invoice` int(11) NOT NULL,
  `last_invoice` int(11) NOT NULL,
  `flat_unique_inv` int(11) NOT NULL,
  `POP_vaaaus_flat_not_exist` int(11) NOT NULL,
  `POP_vaaaus_flat` int(11) NOT NULL,
  `stage_vaaaus_flat` int(11) NOT NULL,
  `sapinv_vaaaus_flat` int(11) NOT NULL,
  `odetails_duplicate` tinyint(4) NOT NULL,
  `chaos_invoice_count` int(11) NOT NULL,
  `stage_invoice_count` int(11) NOT NULL,
  `payments_count` int(11) NOT NULL,
  `payments_detail_count` int(11) NOT NULL,
  `aaa_tt_log` int(11) NOT NULL,
  `replication_log` int(11) NOT NULL,
  `flat_cust` int(11) NOT NULL,
  `cust_db` int(11) NOT NULL,
  `aaa_tt_log_report` int(11) NOT NULL,
  `tt_aaa_report` int(11) NOT NULL,
  `date_rec` datetime NOT NULL,
  `stage_proceedn` int(11) NOT NULL,
  `stage_proceedy` int(11) NOT NULL,
  `stage_invoice_types` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `stage_invoice_OOO` text COLLATE utf8_unicode_ci NOT NULL,
  `dashboard_count` int(11) NOT NULL,
  `error` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`aaa_id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;